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
        bool hasValue = false;
        bool _validAddress = false;
        UInt32 val;
        UInt32 addr;
        List<MEMINFO> Matches = new List<MEMINFO>();
        int MatchIndex = 0;
        bool SearchInitialized = false;

        public MemScanForm()
        {
            InitializeComponent();
            PopulateProcesses();
            MainTabControl.Dock = DockStyle.Fill;
        }
        Process[] ProcessList;
        public void PopulateProcesses()
        {
                ProcessList = Process.GetProcesses();
                ProcessListView.Items.Clear();
                
                foreach (Process proc in ProcessList)
                {

                    ListViewItem Lvi = new ListViewItem(proc.ProcessName);
                    Lvi.SubItems.Add(proc.Id.ToString());
                    Lvi.Tag = proc.Id;
                    ProcessListView.Items.Add(Lvi);

                }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            PopulateProcesses();
        }

        private void ProcessListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
                
            Int32.TryParse(DataBox.Text, out _dataSize);
            if (_dataSize == 0)
            {
                _dataSize = 4;
            }
            if (hasValue) 
            {
                if (!Int32.TryParse(StartValueBox.Text, out _searchValue))
                {
                    MessageBox.Show("Failed to Parse Search Value");
                    return;
                }
            } 
            else 
            {
                _searchValue = 0;   
            }
            this.Cursor = Cursors.WaitCursor;
            PID = (int)ProcessListView.SelectedItems[0].Tag;

            if (hasValue)
            {
                MemoryScan.ConditionalScan((ulong)PID, _dataSize, (UInt32)_searchValue); 
            }
            else
            {
                MemoryScan.UnconditionalScan((ulong)PID, _dataSize);                  
            }
            MainTabControl.SelectTab(1);
            SearchInitialized = true;
            PopulateMatches();
            this.Cursor = Cursors.Default;
        }

        private void DataBox_Enter(object sender, EventArgs e)
        {
            CaptionEnter(DataBox, "[Data Size]");
        }

        private void DataBox_Leave(object sender, EventArgs e)
        {
            CaptionLeave(DataBox, "[Data Size]");
        }

        private void StartValueBox_Enter(object sender, EventArgs e)
        {
            hasValue = true;
            CaptionEnter(StartValueBox, "[Initial Search Value]");
        }

        private void StartValueBox_Leave(object sender, EventArgs e)
        {
            hasValue = !CaptionLeave(StartValueBox, "[Initial Search Value]");   
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
                if (UInt32.TryParse(EditValueBox.Text, out val))
                {
                    if (_validAddress)
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
                    else
                    {
                        MessageBox.Show("Enter A Valid Address");
                    }
                }
                else
                {
                    MessageBox.Show("Enter A Valid Value");
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
                    ListViewItem LVI = new ListViewItem(I.ToString());
                    LVI.SubItems.Add("0x" + Matches[I].addr.ToString("X"));
                    LVI.SubItems.Add(Matches[I].val.ToString());
                    LVI.Tag = Matches[I].addr;
                    MemoryInfoList.Items.Add(LVI); 
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
                    ListViewItem LVI = new ListViewItem(I.ToString());
                    LVI.SubItems.Add("0x" + Matches[I].addr.ToString("X"));
                    LVI.SubItems.Add(Matches[I].val.ToString());
                    LVI.Tag = Matches[I].addr;
                    MemoryInfoList.Items.Add(LVI);   
                }
                MatchIndex = I-1;
            }
        }
        
        void DisplayPrev100()
        {
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
                for ( ; I < MatchIndex - PageI && I < Matches.Count; I++)
                {
                    ListViewItem LVI = new ListViewItem(I.ToString());
                    LVI.SubItems.Add("0x" + Matches[I].addr.ToString("X"));
                    LVI.SubItems.Add(Matches[I].val.ToString());
                    LVI.Tag = Matches[I].addr;
                    MemoryInfoList.Items.Add(LVI);
                }
                MatchIndex = I-1;
            }
        }

        private void LastPage(int lastIndex)
        {
            
            int startIndex = lastIndex;
            if (startIndex % 100 == 99)
            {
                startIndex -= 99;
            }
            else
            {
                startIndex -= startIndex % 100;
            }
            
            MemoryInfoList.Items.Clear();

            for (; startIndex < lastIndex+1 && startIndex < Matches.Count; startIndex++)
            {
                ListViewItem LVI = new ListViewItem(startIndex.ToString());
                LVI.SubItems.Add("0x" + Matches[startIndex].addr.ToString("X"));
                LVI.SubItems.Add(Matches[startIndex].val.ToString());
                LVI.Tag = Matches[startIndex].addr;
                MemoryInfoList.Items.Add(LVI);}
                MatchIndex = startIndex - 1;
        }

        private void Next100_Click(object sender, EventArgs e)
        {
            DisplayNext100();
        }

        private void Prev100_Click(object sender, EventArgs e)
        {
            DisplayPrev100();
        }

        private void refresh_memoryBTN_Click(object sender, EventArgs e)
        {
            if (SearchInitialized)
            {
                int PagePos = MatchIndex;
                PopulateMatches();
                LastPage(PagePos);
            }

        }

        private void AddressBox_Enter(object sender, EventArgs e)
        {
            CaptionEnter(AddressBox, "[Address]");
        }

        private void AddressBox_Leave(object sender, EventArgs e)
        {
            CaptionLeave(AddressBox, "[Address]");
        }

        private void EditValueBox_Enter(object sender, EventArgs e)
        {
            CaptionEnter(EditValueBox, "[Value]");
        }

        private void EditValueBox_Leave(object sender, EventArgs e)
        {
            CaptionLeave(EditValueBox, "[Value]");

        }

        private void MemoryInfoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MemoryInfoList.SelectedItems.Count > 0)
            {
                uint MemValue = (uint)MemoryInfoList.SelectedItems[0].Tag;
                AddressBox.Text = "0x" + MemValue.ToString("X");
            }
        }

        private void AddressBox_TextChanged(object sender, EventArgs e)
        {
            _validAddress = false;
            string HexAddress = AddressBox.Text;
            if(HexAddress.Length > 2 && HexAddress.Substring(0, 2) == "0x")
            {
                HexAddress = HexAddress.Substring(2, HexAddress.Length-2);
            }
            try
            {
                addr = Convert.ToUInt32(HexAddress, 16);
                _validAddress = true;
            }
            catch
            {}
        }

        private void SpecificTextBox_Enter(object sender, EventArgs e)
        {
            CaptionEnter(SpecificTextBox, "[Search Value]");
        }

        private void SpecificTextBox_Leave(object sender, EventArgs e)
        {
            CaptionLeave(SpecificTextBox, "[Search Value]");
        }

        private void SpecificBTN_Click(object sender, EventArgs e)
        {
            if (UInt32.TryParse(SpecificTextBox.Text, out val))
            {
                MemoryScan.ConditionEquals(val);
                PopulateMatches();
            }
        }

        private void MainTabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabPage Current = (sender as TabControl).SelectedTab;
            if (Current == MainTabControl.Controls[0])
            {
                DialogResult dialogResult = MessageBox.Show("Changing Tabs Will Lose Current Results, Proceed to Change Tab?", "Data Loss Prevention", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MemoryScan.Free_Scan();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void ChangedbyBox_Enter(object sender, EventArgs e)
        {
            CaptionEnter(ChangedbyBox, "[Changed by]");
        }

        private void ChangedbyBox_Leave(object sender, EventArgs e)
        {
            CaptionLeave(ChangedbyBox, "[Changed by]");
        }

        private void BetweenBox_Enter(object sender, EventArgs e)
        {
            CaptionEnter(BetweenBox, "[Greater]-[Less]");
        }

        private void BetweenBox_Leave(object sender, EventArgs e)
        {
            CaptionLeave(BetweenBox, "[Greater]-[Less]");
        }

        private bool CaptionEnter(TextBox T, string Caption)
        {
            bool TextChanged = false;
            if (T.Text == Caption)
            {
                T.ForeColor = Color.Black;
                T.Text = "";
                TextChanged = true;
            }
            return TextChanged;
        }
        private bool CaptionLeave(TextBox T, string Caption)
        {
            bool TextChanged = false;            
            if (T.Text == "")
            {
                T.ForeColor = Color.Gray;
                T.Text = Caption;
                TextChanged = true;
            }
            return TextChanged;
        }

    }
}
