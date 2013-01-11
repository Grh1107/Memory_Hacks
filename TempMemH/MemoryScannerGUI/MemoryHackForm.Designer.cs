namespace MemoryScannerGUI
{
    partial class MemScanForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProcessListView = new System.Windows.Forms.ListView();
            this.ProcessCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PIDCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.ProcessTab = new System.Windows.Forms.TabPage();
            this.DataBox = new System.Windows.Forms.TextBox();
            this.StartValueBox = new System.Windows.Forms.TextBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.MemoryTab = new System.Windows.Forms.TabPage();
            this.SpecificTextBox = new System.Windows.Forms.TextBox();
            this.SpecificBTN = new System.Windows.Forms.Button();
            this.AddressBox = new System.Windows.Forms.TextBox();
            this.refresh_memoryBTN = new System.Windows.Forms.Button();
            this.Prev100 = new System.Windows.Forms.Button();
            this.Next100 = new System.Windows.Forms.Button();
            this.MatchNumLB = new System.Windows.Forms.Label();
            this.NumOfMatchLB = new System.Windows.Forms.Label();
            this.EditValueBtn = new System.Windows.Forms.Button();
            this.EditValueBox = new System.Windows.Forms.TextBox();
            this.BetweenBox = new System.Windows.Forms.TextBox();
            this.LessThanBtn = new System.Windows.Forms.Button();
            this.GreaterThanBtn = new System.Windows.Forms.Button();
            this.RemainedBtn = new System.Windows.Forms.Button();
            this.ChangedButton = new System.Windows.Forms.Button();
            this.ChangedbyBox = new System.Windows.Forms.TextBox();
            this.DecreasedbyBTN = new System.Windows.Forms.Button();
            this.IncreasedByButton = new System.Windows.Forms.Button();
            this.DecreaseButton = new System.Windows.Forms.Button();
            this.Increasedbutton = new System.Windows.Forms.Button();
            this.MemoryInfoList = new System.Windows.Forms.ListView();
            this.IndexOfMatch = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddrCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueCol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.MainTabControl.SuspendLayout();
            this.ProcessTab.SuspendLayout();
            this.MemoryTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessListView
            // 
            this.ProcessListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProcessCol,
            this.PIDCol});
            this.ProcessListView.HideSelection = false;
            this.ProcessListView.Location = new System.Drawing.Point(3, 3);
            this.ProcessListView.Name = "ProcessListView";
            this.ProcessListView.Size = new System.Drawing.Size(386, 341);
            this.ProcessListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ProcessListView.TabIndex = 0;
            this.ProcessListView.UseCompatibleStateImageBehavior = false;
            this.ProcessListView.View = System.Windows.Forms.View.Details;
            this.ProcessListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ProcessListView_MouseDoubleClick);
            // 
            // ProcessCol
            // 
            this.ProcessCol.Text = "Process";
            this.ProcessCol.Width = 190;
            // 
            // PIDCol
            // 
            this.PIDCol.Text = "PID";
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.ProcessTab);
            this.MainTabControl.Controls.Add(this.MemoryTab);
            this.MainTabControl.Location = new System.Drawing.Point(0, 1);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(400, 408);
            this.MainTabControl.TabIndex = 2;
            this.MainTabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.MainTabControl_Selecting);
            // 
            // ProcessTab
            // 
            this.ProcessTab.Controls.Add(this.DataBox);
            this.ProcessTab.Controls.Add(this.StartValueBox);
            this.ProcessTab.Controls.Add(this.ProcessListView);
            this.ProcessTab.Controls.Add(this.RefreshButton);
            this.ProcessTab.Location = new System.Drawing.Point(4, 22);
            this.ProcessTab.Name = "ProcessTab";
            this.ProcessTab.Padding = new System.Windows.Forms.Padding(3);
            this.ProcessTab.Size = new System.Drawing.Size(392, 382);
            this.ProcessTab.TabIndex = 0;
            this.ProcessTab.Text = "Select Process";
            this.ProcessTab.UseVisualStyleBackColor = true;
            // 
            // DataBox
            // 
            this.DataBox.ForeColor = System.Drawing.Color.Gray;
            this.DataBox.Location = new System.Drawing.Point(137, 353);
            this.DataBox.Name = "DataBox";
            this.DataBox.Size = new System.Drawing.Size(106, 20);
            this.DataBox.TabIndex = 4;
            this.DataBox.Text = "Data Size";
            this.DataBox.Enter += new System.EventHandler(this.DataBox_Enter);
            this.DataBox.Leave += new System.EventHandler(this.DataBox_Leave);
            // 
            // StartValueBox
            // 
            this.StartValueBox.ForeColor = System.Drawing.Color.Gray;
            this.StartValueBox.Location = new System.Drawing.Point(281, 353);
            this.StartValueBox.Name = "StartValueBox";
            this.StartValueBox.Size = new System.Drawing.Size(105, 20);
            this.StartValueBox.TabIndex = 1;
            this.StartValueBox.Text = "Initial Search Value";
            this.StartValueBox.Enter += new System.EventHandler(this.StartValueBox_Enter);
            this.StartValueBox.Leave += new System.EventHandler(this.StartValueBox_Leave);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Location = new System.Drawing.Point(17, 351);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 3;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // MemoryTab
            // 
            this.MemoryTab.BackColor = System.Drawing.Color.White;
            this.MemoryTab.Controls.Add(this.SpecificTextBox);
            this.MemoryTab.Controls.Add(this.SpecificBTN);
            this.MemoryTab.Controls.Add(this.AddressBox);
            this.MemoryTab.Controls.Add(this.refresh_memoryBTN);
            this.MemoryTab.Controls.Add(this.Prev100);
            this.MemoryTab.Controls.Add(this.Next100);
            this.MemoryTab.Controls.Add(this.MatchNumLB);
            this.MemoryTab.Controls.Add(this.NumOfMatchLB);
            this.MemoryTab.Controls.Add(this.EditValueBtn);
            this.MemoryTab.Controls.Add(this.EditValueBox);
            this.MemoryTab.Controls.Add(this.BetweenBox);
            this.MemoryTab.Controls.Add(this.LessThanBtn);
            this.MemoryTab.Controls.Add(this.GreaterThanBtn);
            this.MemoryTab.Controls.Add(this.RemainedBtn);
            this.MemoryTab.Controls.Add(this.ChangedButton);
            this.MemoryTab.Controls.Add(this.ChangedbyBox);
            this.MemoryTab.Controls.Add(this.DecreasedbyBTN);
            this.MemoryTab.Controls.Add(this.IncreasedByButton);
            this.MemoryTab.Controls.Add(this.DecreaseButton);
            this.MemoryTab.Controls.Add(this.Increasedbutton);
            this.MemoryTab.Controls.Add(this.MemoryInfoList);
            this.MemoryTab.Controls.Add(this.shapeContainer1);
            this.MemoryTab.Location = new System.Drawing.Point(4, 22);
            this.MemoryTab.Name = "MemoryTab";
            this.MemoryTab.Padding = new System.Windows.Forms.Padding(3);
            this.MemoryTab.Size = new System.Drawing.Size(392, 382);
            this.MemoryTab.TabIndex = 1;
            this.MemoryTab.Text = "Memory";
            // 
            // SpecificTextBox
            // 
            this.SpecificTextBox.ForeColor = System.Drawing.Color.Gray;
            this.SpecificTextBox.Location = new System.Drawing.Point(295, 282);
            this.SpecificTextBox.MaxLength = 10;
            this.SpecificTextBox.Name = "SpecificTextBox";
            this.SpecificTextBox.Size = new System.Drawing.Size(91, 20);
            this.SpecificTextBox.TabIndex = 20;
            this.SpecificTextBox.Text = "[Search Value]";
            this.SpecificTextBox.Enter += new System.EventHandler(this.SpecificTextBox_Enter);
            this.SpecificTextBox.Leave += new System.EventHandler(this.SpecificTextBox_Leave);
            // 
            // SpecificBTN
            // 
            this.SpecificBTN.Location = new System.Drawing.Point(207, 279);
            this.SpecificBTN.Name = "SpecificBTN";
            this.SpecificBTN.Size = new System.Drawing.Size(75, 23);
            this.SpecificBTN.TabIndex = 19;
            this.SpecificBTN.Text = "Specific";
            this.SpecificBTN.UseVisualStyleBackColor = true;
            this.SpecificBTN.Click += new System.EventHandler(this.SpecificBTN_Click);
            // 
            // AddressBox
            // 
            this.AddressBox.ForeColor = System.Drawing.Color.Gray;
            this.AddressBox.Location = new System.Drawing.Point(295, 216);
            this.AddressBox.MaxLength = 10;
            this.AddressBox.Name = "AddressBox";
            this.AddressBox.Size = new System.Drawing.Size(91, 20);
            this.AddressBox.TabIndex = 18;
            this.AddressBox.Text = "[Address]";
            this.AddressBox.TextChanged += new System.EventHandler(this.AddressBox_TextChanged);
            this.AddressBox.Enter += new System.EventHandler(this.AddressBox_Enter);
            this.AddressBox.Leave += new System.EventHandler(this.AddressBox_Leave);
            // 
            // refresh_memoryBTN
            // 
            this.refresh_memoryBTN.Location = new System.Drawing.Point(207, 324);
            this.refresh_memoryBTN.Name = "refresh_memoryBTN";
            this.refresh_memoryBTN.Size = new System.Drawing.Size(75, 23);
            this.refresh_memoryBTN.TabIndex = 13;
            this.refresh_memoryBTN.Text = "Refresh";
            this.refresh_memoryBTN.UseVisualStyleBackColor = true;
            this.refresh_memoryBTN.Click += new System.EventHandler(this.refresh_memoryBTN_Click);
            // 
            // Prev100
            // 
            this.Prev100.Location = new System.Drawing.Point(207, 353);
            this.Prev100.Name = "Prev100";
            this.Prev100.Size = new System.Drawing.Size(75, 23);
            this.Prev100.TabIndex = 15;
            this.Prev100.Text = "Prev 100";
            this.Prev100.UseVisualStyleBackColor = true;
            this.Prev100.Click += new System.EventHandler(this.Prev100_Click);
            // 
            // Next100
            // 
            this.Next100.Location = new System.Drawing.Point(317, 353);
            this.Next100.Name = "Next100";
            this.Next100.Size = new System.Drawing.Size(75, 23);
            this.Next100.TabIndex = 16;
            this.Next100.Text = "Next 100";
            this.Next100.UseVisualStyleBackColor = true;
            this.Next100.Click += new System.EventHandler(this.Next100_Click);
            // 
            // MatchNumLB
            // 
            this.MatchNumLB.AutoSize = true;
            this.MatchNumLB.Location = new System.Drawing.Point(317, 9);
            this.MatchNumLB.Name = "MatchNumLB";
            this.MatchNumLB.Size = new System.Drawing.Size(0, 13);
            this.MatchNumLB.TabIndex = 14;
            // 
            // NumOfMatchLB
            // 
            this.NumOfMatchLB.AutoSize = true;
            this.NumOfMatchLB.Location = new System.Drawing.Point(210, 9);
            this.NumOfMatchLB.Name = "NumOfMatchLB";
            this.NumOfMatchLB.Size = new System.Drawing.Size(105, 13);
            this.NumOfMatchLB.TabIndex = 13;
            this.NumOfMatchLB.Text = "Number of matches :";
            // 
            // EditValueBtn
            // 
            this.EditValueBtn.Location = new System.Drawing.Point(207, 226);
            this.EditValueBtn.Name = "EditValueBtn";
            this.EditValueBtn.Size = new System.Drawing.Size(82, 23);
            this.EditValueBtn.TabIndex = 12;
            this.EditValueBtn.Text = "Edit Value";
            this.EditValueBtn.UseVisualStyleBackColor = true;
            this.EditValueBtn.Click += new System.EventHandler(this.EditValueBtn_Click);
            // 
            // EditValueBox
            // 
            this.EditValueBox.ForeColor = System.Drawing.Color.Gray;
            this.EditValueBox.Location = new System.Drawing.Point(295, 242);
            this.EditValueBox.MaxLength = 10;
            this.EditValueBox.Name = "EditValueBox";
            this.EditValueBox.Size = new System.Drawing.Size(91, 20);
            this.EditValueBox.TabIndex = 11;
            this.EditValueBox.Text = "[Value]";
            this.EditValueBox.Enter += new System.EventHandler(this.EditValueBox_Enter);
            this.EditValueBox.Leave += new System.EventHandler(this.EditValueBox_Leave);
            // 
            // BetweenBox
            // 
            this.BetweenBox.Location = new System.Drawing.Point(250, 174);
            this.BetweenBox.MaxLength = 10;
            this.BetweenBox.Name = "BetweenBox";
            this.BetweenBox.Size = new System.Drawing.Size(94, 20);
            this.BetweenBox.TabIndex = 9;
            // 
            // LessThanBtn
            // 
            this.LessThanBtn.Font = new System.Drawing.Font("Monotype Corsiva", 17.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.LessThanBtn.Location = new System.Drawing.Point(347, 164);
            this.LessThanBtn.Name = "LessThanBtn";
            this.LessThanBtn.Size = new System.Drawing.Size(42, 35);
            this.LessThanBtn.TabIndex = 10;
            this.LessThanBtn.Text = "<";
            this.LessThanBtn.UseVisualStyleBackColor = true;
            this.LessThanBtn.Click += new System.EventHandler(this.LessThanBtn_Click);
            // 
            // GreaterThanBtn
            // 
            this.GreaterThanBtn.Font = new System.Drawing.Font("Monotype Corsiva", 17.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.GreaterThanBtn.Location = new System.Drawing.Point(205, 164);
            this.GreaterThanBtn.Name = "GreaterThanBtn";
            this.GreaterThanBtn.Size = new System.Drawing.Size(42, 35);
            this.GreaterThanBtn.TabIndex = 8;
            this.GreaterThanBtn.Text = ">";
            this.GreaterThanBtn.UseVisualStyleBackColor = true;
            this.GreaterThanBtn.Click += new System.EventHandler(this.GreaterThanBtn_Click);
            // 
            // RemainedBtn
            // 
            this.RemainedBtn.Location = new System.Drawing.Point(213, 69);
            this.RemainedBtn.Name = "RemainedBtn";
            this.RemainedBtn.Size = new System.Drawing.Size(75, 23);
            this.RemainedBtn.TabIndex = 3;
            this.RemainedBtn.Text = "Remained";
            this.RemainedBtn.UseVisualStyleBackColor = true;
            this.RemainedBtn.Click += new System.EventHandler(this.RemainedBtn_Click);
            // 
            // ChangedButton
            // 
            this.ChangedButton.Location = new System.Drawing.Point(311, 69);
            this.ChangedButton.Name = "ChangedButton";
            this.ChangedButton.Size = new System.Drawing.Size(75, 23);
            this.ChangedButton.TabIndex = 4;
            this.ChangedButton.Text = "Changed";
            this.ChangedButton.UseVisualStyleBackColor = true;
            this.ChangedButton.Click += new System.EventHandler(this.ChangedButton_Click);
            // 
            // ChangedbyBox
            // 
            this.ChangedbyBox.Location = new System.Drawing.Point(248, 117);
            this.ChangedbyBox.MaxLength = 10;
            this.ChangedbyBox.Name = "ChangedbyBox";
            this.ChangedbyBox.Size = new System.Drawing.Size(99, 20);
            this.ChangedbyBox.TabIndex = 5;
            // 
            // DecreasedbyBTN
            // 
            this.DecreasedbyBTN.Font = new System.Drawing.Font("Monotype Corsiva", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecreasedbyBTN.Location = new System.Drawing.Point(347, 117);
            this.DecreasedbyBTN.Name = "DecreasedbyBTN";
            this.DecreasedbyBTN.Size = new System.Drawing.Size(42, 35);
            this.DecreasedbyBTN.TabIndex = 7;
            this.DecreasedbyBTN.Text = "↓";
            this.DecreasedbyBTN.UseVisualStyleBackColor = true;
            this.DecreasedbyBTN.Click += new System.EventHandler(this.DecreasedbyBTN_Click);
            // 
            // IncreasedByButton
            // 
            this.IncreasedByButton.Font = new System.Drawing.Font("Monotype Corsiva", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IncreasedByButton.Location = new System.Drawing.Point(205, 117);
            this.IncreasedByButton.Name = "IncreasedByButton";
            this.IncreasedByButton.Size = new System.Drawing.Size(42, 35);
            this.IncreasedByButton.TabIndex = 5;
            this.IncreasedByButton.Text = "↑";
            this.IncreasedByButton.UseVisualStyleBackColor = true;
            this.IncreasedByButton.Click += new System.EventHandler(this.IncreasedByButton_Click);
            // 
            // DecreaseButton
            // 
            this.DecreaseButton.Location = new System.Drawing.Point(311, 30);
            this.DecreaseButton.Name = "DecreaseButton";
            this.DecreaseButton.Size = new System.Drawing.Size(75, 23);
            this.DecreaseButton.TabIndex = 2;
            this.DecreaseButton.Text = "Decreased";
            this.DecreaseButton.UseVisualStyleBackColor = true;
            this.DecreaseButton.Click += new System.EventHandler(this.DecreaseButton_Click);
            // 
            // Increasedbutton
            // 
            this.Increasedbutton.Location = new System.Drawing.Point(213, 30);
            this.Increasedbutton.Name = "Increasedbutton";
            this.Increasedbutton.Size = new System.Drawing.Size(75, 23);
            this.Increasedbutton.TabIndex = 1;
            this.Increasedbutton.Text = "Increased";
            this.Increasedbutton.UseVisualStyleBackColor = true;
            this.Increasedbutton.Click += new System.EventHandler(this.Increasedbutton_Click);
            // 
            // MemoryInfoList
            // 
            this.MemoryInfoList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IndexOfMatch,
            this.AddrCol,
            this.ValueCol});
            this.MemoryInfoList.HideSelection = false;
            this.MemoryInfoList.Location = new System.Drawing.Point(-4, 0);
            this.MemoryInfoList.Name = "MemoryInfoList";
            this.MemoryInfoList.Size = new System.Drawing.Size(205, 382);
            this.MemoryInfoList.TabIndex = 0;
            this.MemoryInfoList.UseCompatibleStateImageBehavior = false;
            this.MemoryInfoList.View = System.Windows.Forms.View.Details;
            this.MemoryInfoList.SelectedIndexChanged += new System.EventHandler(this.MemoryInfoList_SelectedIndexChanged);
            // 
            // IndexOfMatch
            // 
            this.IndexOfMatch.Text = "#";
            this.IndexOfMatch.Width = 34;
            // 
            // AddrCol
            // 
            this.AddrCol.Text = "Address";
            this.AddrCol.Width = 84;
            // 
            // ValueCol
            // 
            this.ValueCol.Text = "Value";
            this.ValueCol.Width = 83;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 3);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(386, 376);
            this.shapeContainer1.TabIndex = 17;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = -3;
            this.lineShape1.X2 = 72;
            this.lineShape1.Y1 = -3;
            this.lineShape1.Y2 = 20;
            // 
            // MemScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(402, 407);
            this.Controls.Add(this.MainTabControl);
            this.Name = "MemScanForm";
            this.Text = "Memory Scanner";
            this.MainTabControl.ResumeLayout(false);
            this.ProcessTab.ResumeLayout(false);
            this.ProcessTab.PerformLayout();
            this.MemoryTab.ResumeLayout(false);
            this.MemoryTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ProcessListView;
        private System.Windows.Forms.ColumnHeader ProcessCol;
        private System.Windows.Forms.ColumnHeader PIDCol;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage ProcessTab;
        private System.Windows.Forms.TabPage MemoryTab;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.TextBox StartValueBox;
        private System.Windows.Forms.TextBox DataBox;
        private System.Windows.Forms.Button DecreaseButton;
        private System.Windows.Forms.Button Increasedbutton;
        private System.Windows.Forms.ListView MemoryInfoList;
        private System.Windows.Forms.ColumnHeader IndexOfMatch;
        private System.Windows.Forms.ColumnHeader AddrCol;
        private System.Windows.Forms.TextBox ChangedbyBox;
        private System.Windows.Forms.Button DecreasedbyBTN;
        private System.Windows.Forms.Button IncreasedByButton;
        private System.Windows.Forms.Button RemainedBtn;
        private System.Windows.Forms.Button ChangedButton;
        private System.Windows.Forms.TextBox BetweenBox;
        private System.Windows.Forms.Button LessThanBtn;
        private System.Windows.Forms.Button GreaterThanBtn;
        private System.Windows.Forms.Button EditValueBtn;
        private System.Windows.Forms.TextBox EditValueBox;
        private System.Windows.Forms.Label MatchNumLB;
        private System.Windows.Forms.Label NumOfMatchLB;
        private System.Windows.Forms.ColumnHeader ValueCol;
        private System.Windows.Forms.Button Prev100;
        private System.Windows.Forms.Button Next100;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Button refresh_memoryBTN;
        private System.Windows.Forms.TextBox AddressBox;
        private System.Windows.Forms.TextBox SpecificTextBox;
        private System.Windows.Forms.Button SpecificBTN;
    }
}

