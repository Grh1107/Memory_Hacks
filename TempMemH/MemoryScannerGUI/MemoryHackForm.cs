using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MemoryScannerGUI
{
    public partial class MemScanForm : Form
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct MEMINFO
        {
            public UInt32 addr;
            public UInt32 val;
        }

        int PID;
        int _dataSize;
        int _searchValue;
        bool _validInfo = true;
        bool hasData = false;
        bool hasValue = false;
        UInt32 val;
        UInt32 addr;
        List<MEMINFO> Matches = new List<MEMINFO>();
        int MatchIndex = 0;

        public MemScanForm()
        {
            InitializeComponent();
            PopulateProcesses();
            MainTabControl.Dock = DockStyle.Fill;
        }
        Process[] ProcessList;
        int TotalProcesses = 0;
        public void PopulateProcesses()
        {
            ProcessList = Process.GetProcesses();
                ProcessListView.Items.Clear();
                TotalProcesses = 0;
                foreach (Process proc in ProcessList)
                {

                    ProcessListView.Items.Add(proc.ProcessName);
                    ProcessListView.Items[TotalProcesses].SubItems.Add(proc.Id.ToString());
                    ProcessListView.Items[TotalProcesses].Tag = proc.Id;
                    ++TotalProcesses;
                }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            PopulateProcesses();
        }

        private void ProcessListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_validInfo)
            {
                _dataSize = (hasData) ? Convert.ToInt32(DataBox.Text) : 4;
                if (hasValue) 
                {
                    _searchValue = Convert.ToInt32(StartValueBox.Text);
                } 
                else 
                {
                    _searchValue = 0;   
                }
                PID = (int)ProcessListView.SelectedItems[0].Tag;

                MainTabControl.SelectTab(1);
                if (hasValue)
                {
                    MemoryScan.ConditionalScan((ulong)PID, _dataSize, (UInt32)_searchValue); 
                }
                else
                {
                    MemoryScan.UnconditionalScan((ulong)PID, _dataSize);                  
                }
                PopulateMatches();
                Console.WriteLine(MemoryScan.MatchCount());
            }
        }

        private void DataBox_Enter(object sender, EventArgs e)
        {
            DataBox.Text = "";
            DataBox.ForeColor = Color.Black;
            hasData = true;
        }

        private void DataBox_Leave(object sender, EventArgs e)
        {
            DataBox.ForeColor = Color.Gray;
            if (DataBox.Text == "")
            {
                DataBox.Text = "Data Size";
                hasData = false;
            }
        }

        private void StartValueBox_Enter(object sender, EventArgs e)
        {
            StartValueBox.Text = "";
            StartValueBox.ForeColor = Color.Black;
            hasValue = true;
        }

        private void StartValueBox_Leave(object sender, EventArgs e)
        {
            StartValueBox.ForeColor = Color.Gray;
            if (StartValueBox.Text == "")
            {
                StartValueBox.Text = "Initial Search Value";
                hasValue = false;
            }
        }

        private void Increasedbutton_Click(object sender, EventArgs e)
        {
            MemoryScan.Increased();
            PopulateMatches();
        }

        private void DecreaseButton_Click(object sender, EventArgs e)
        {
            MemoryScan.Decreased();
            PopulateMatches();
        }

        private void ChangedButton_Click(object sender, EventArgs e)
        {
            MemoryScan.Changed();
            PopulateMatches();
        }

        private void RemainedBtn_Click(object sender, EventArgs e)
        {
            MemoryScan.Remained();
            PopulateMatches();
        }

        private void IncreasedByButton_Click(object sender, EventArgs e)
        {
            if (UInt32.TryParse(ChangedbyBox.Text, out val))
            {
                MemoryScan.IncreasedBy(val);
                PopulateMatches();
            }
        }

        private void DecreasedbyBTN_Click(object sender, EventArgs e)
        {
            if (UInt32.TryParse(ChangedbyBox.Text, out val))
            {
                MemoryScan.DecreasedBy(val);
                PopulateMatches();
            }
        }

        private void LessThanBtn_Click(object sender, EventArgs e)
        {
            if (UInt32.TryParse(BetweenBox.Text, out val))
            {
                MemoryScan.LessThan(val);
                PopulateMatches();
            }
        }

        private void GreaterThanBtn_Click(object sender, EventArgs e)
        {
            if (UInt32.TryParse(BetweenBox.Text, out val))
            {
                MemoryScan.GreaterThan(val);
                PopulateMatches();
            }
        }

        private void EditValueBtn_Click(object sender, EventArgs e)
        {
            if (MemoryInfoList.SelectedItems.Count > 0)
            {
                addr = (uint)MemoryInfoList.SelectedItems[0].Tag;
                if (UInt32.TryParse(EditValueBox.Text, out val))
                {
                    if (MemoryScan.WriteAddress(addr, val))
                    {
                        PopulateMatches();
                    }
                    else
                    {
                        MessageBox.Show("Failed to Write to Memory");
                    }
                }
            }
        }

        private void PopulateMatches()
        {
            Matches.Clear();
            int MC = MemoryScan.MatchCount();
            MatchNumLB.Text = MC.ToString();
            IntPtr MatchRes = MemoryScan.MatchResult();

            if (MatchRes == IntPtr.Zero)
            {
                MessageBox.Show("Failed to allocate Memory");
                return;
            }

            MemoryInfoList.Items.Clear();

            for (int I = 0; I < MC; I++)
            {
                int MISS = Marshal.SizeOf(typeof(MEMINFO));
                Matches.Add((MEMINFO)Marshal.PtrToStructure(MatchRes, typeof(MEMINFO)));
                MatchRes = IntPtr.Add(MatchRes, MISS);
                
                if (I < 100)
                {
                    MemoryInfoList.Items.Add(I.ToString());
                    MemoryInfoList.Items[I].SubItems.Add("0x" + Matches[I].addr.ToString("X"));
                    MemoryInfoList.Items[I].SubItems.Add(Matches[I].val.ToString());
                    MemoryInfoList.Items[I].Tag = Matches[I].addr;
                    MatchIndex = I;
                }
            }
            MemoryScan.Free_Returned_Matches();
        }
        void DisplayNext100()
        {
            if (MatchIndex < Matches.Count-1)
            {
                MemoryInfoList.Items.Clear();
                int I;
                for (I = MatchIndex+1; I <= MatchIndex + 100 && I < Matches.Count; I++)
                {
                    MemoryInfoList.Items.Add(I.ToString());
                    MemoryInfoList.Items[I%100].SubItems.Add("0x" + Matches[I].addr.ToString("X"));
                    MemoryInfoList.Items[I%100].SubItems.Add(Matches[I].val.ToString());
                    MemoryInfoList.Items[I%100].Tag = Matches[I].addr;   
                }
                MatchIndex = I-1;
            }
        }
        
        void DisplayPrev100()
        {
            //broken
            if (MatchIndex > 99)
            {
                MemoryInfoList.Items.Clear();
                
                int I = MatchIndex - 99;
                int PageI = 99;
                if (I % 100 != 0)
                {
                    I = I-I%100;
                    PageI = MatchIndex % 100;
                }
                else
                {
                    I -= 100;
                }
                for ( ; I < MatchIndex - PageI && I < Matches.Count - 1; I++)
                {
                    MemoryInfoList.Items.Add(I.ToString());
                    MemoryInfoList.Items[I%100].SubItems.Add("0x" + Matches[I].addr.ToString("X"));
                    MemoryInfoList.Items[I % 100].SubItems.Add(Matches[I].val.ToString());
                    MemoryInfoList.Items[I % 100].Tag = Matches[I].addr;
                }
                MatchIndex = I-1;
            }
        }

        private void Next100_Click(object sender, EventArgs e)
        {
            DisplayNext100();
        }

        private void Prev100_Click(object sender, EventArgs e)
        {
            DisplayPrev100();
        }
    }
}
