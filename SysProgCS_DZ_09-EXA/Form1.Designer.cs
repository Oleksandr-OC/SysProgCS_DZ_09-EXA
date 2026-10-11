namespace SysProgCS_DZ_09_EXA
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timerDetection?.Stop();
                timerUpdate?.Stop();
                ClearProcesses();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            listBoxProcesses = new ListBox();
            buttonUpdate = new Button();
            buttonStop = new Button();
            label1 = new Label();
            textBoxInfo = new TextBox();
            groupBoxBlackList = new GroupBox();
            listBoxBlackList = new ListBox();
            buttonDetection = new Button();
            buttonRemove = new Button();
            buttonAdd = new Button();
            textBoxBlackList = new TextBox();
            label2 = new Label();
            textBoxInput = new TextBox();
            buttonStart = new Button();
            timerDetection = new System.Windows.Forms.Timer(components);
            timerUpdate = new System.Windows.Forms.Timer(components);
            buttonLog = new Button();
            buttonSettings = new Button();
            checkBoxAutoStart = new CheckBox();
            checkBoxRestart = new CheckBox();
            buttonExit = new Button();
            labelStatus = new Label();
            groupBoxBlackList.SuspendLayout();
            SuspendLayout();
            // 
            // listBoxProcesses
            // 
            listBoxProcesses.FormattingEnabled = true;
            listBoxProcesses.HorizontalScrollbar = true;
            listBoxProcesses.ItemHeight = 25;
            listBoxProcesses.Location = new Point(15, 62);
            listBoxProcesses.Margin = new Padding(4);
            listBoxProcesses.Name = "listBoxProcesses";
            listBoxProcesses.Size = new Size(284, 554);
            listBoxProcesses.Sorted = true;
            listBoxProcesses.TabIndex = 0;
            listBoxProcesses.SelectedIndexChanged += listBoxProcesses_SelectedIndexChanged;
            // 
            // buttonUpdate
            // 
            buttonUpdate.Location = new Point(15, 15);
            buttonUpdate.Margin = new Padding(4);
            buttonUpdate.Name = "buttonUpdate";
            buttonUpdate.Size = new Size(138, 36);
            buttonUpdate.TabIndex = 1;
            buttonUpdate.Text = "Update";
            buttonUpdate.UseVisualStyleBackColor = true;
            buttonUpdate.Click += buttonUpdate_Click;
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(162, 15);
            buttonStop.Margin = new Padding(4);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(138, 36);
            buttonStop.TabIndex = 2;
            buttonStop.Text = "Stop";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += buttonStop_Click;
            // 
            // label1
            // 
            label1.Location = new Point(331, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(188, 25);
            label1.TabIndex = 3;
            label1.Text = "Process information";
            // 
            // textBoxInfo
            // 
            textBoxInfo.Location = new Point(331, 62);
            textBoxInfo.Margin = new Padding(4);
            textBoxInfo.Multiline = true;
            textBoxInfo.Name = "textBoxInfo";
            textBoxInfo.ReadOnly = true;
            textBoxInfo.ScrollBars = ScrollBars.Vertical;
            textBoxInfo.Size = new Size(653, 236);
            textBoxInfo.TabIndex = 4;
            // 
            // groupBoxBlackList
            // 
            groupBoxBlackList.Controls.Add(listBoxBlackList);
            groupBoxBlackList.Controls.Add(buttonDetection);
            groupBoxBlackList.Controls.Add(buttonRemove);
            groupBoxBlackList.Controls.Add(buttonAdd);
            groupBoxBlackList.Controls.Add(textBoxBlackList);
            groupBoxBlackList.Location = new Point(319, 319);
            groupBoxBlackList.Margin = new Padding(4);
            groupBoxBlackList.Name = "groupBoxBlackList";
            groupBoxBlackList.Padding = new Padding(4);
            groupBoxBlackList.Size = new Size(666, 238);
            groupBoxBlackList.TabIndex = 5;
            groupBoxBlackList.TabStop = false;
            groupBoxBlackList.Text = "Black list";
            // 
            // listBoxBlackList
            // 
            listBoxBlackList.FormattingEnabled = true;
            listBoxBlackList.HorizontalScrollbar = true;
            listBoxBlackList.ItemHeight = 25;
            listBoxBlackList.Location = new Point(19, 38);
            listBoxBlackList.Margin = new Padding(4);
            listBoxBlackList.Name = "listBoxBlackList";
            listBoxBlackList.Size = new Size(236, 179);
            listBoxBlackList.TabIndex = 6;
            // 
            // buttonDetection
            // 
            buttonDetection.Location = new Point(275, 38);
            buttonDetection.Margin = new Padding(4);
            buttonDetection.Name = "buttonDetection";
            buttonDetection.Size = new Size(225, 56);
            buttonDetection.TabIndex = 7;
            buttonDetection.Text = "Start apps detection";
            buttonDetection.UseVisualStyleBackColor = true;
            buttonDetection.Click += buttonDetection_Click;
            // 
            // buttonRemove
            // 
            buttonRemove.Location = new Point(522, 38);
            buttonRemove.Margin = new Padding(4);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(125, 36);
            buttonRemove.TabIndex = 8;
            buttonRemove.Text = "Remove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new Point(522, 88);
            buttonAdd.Margin = new Padding(4);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(125, 36);
            buttonAdd.TabIndex = 9;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // textBoxBlackList
            // 
            textBoxBlackList.Location = new Point(275, 162);
            textBoxBlackList.Margin = new Padding(4);
            textBoxBlackList.Name = "textBoxBlackList";
            textBoxBlackList.Size = new Size(372, 31);
            textBoxBlackList.TabIndex = 10;
            // 
            // label2
            // 
            label2.Location = new Point(319, 574);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(375, 25);
            label2.TabIndex = 11;
            label2.Text = "Process name or file path";
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(319, 612);
            textBoxInput.Margin = new Padding(4);
            textBoxInput.Name = "textBoxInput";
            textBoxInput.Size = new Size(528, 31);
            textBoxInput.TabIndex = 12;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(868, 611);
            buttonStart.Margin = new Padding(4);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(118, 36);
            buttonStart.TabIndex = 13;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // timerDetection
            // 
            timerDetection.Interval = 1000;
            timerDetection.Tick += timerDetection_Tick;
            // 
            // timerUpdate
            // 
            timerUpdate.Interval = 2000;
            timerUpdate.Tick += timerUpdate_Tick;
            // 
            // buttonLog
            // 
            buttonLog.Location = new Point(15, 704);
            buttonLog.Name = "buttonLog";
            buttonLog.Size = new Size(138, 36);
            buttonLog.TabIndex = 14;
            buttonLog.Text = "Open log";
            buttonLog.UseVisualStyleBackColor = true;
            buttonLog.Click += buttonLog_Click;
            // 
            // buttonSettings
            // 
            buttonSettings.Location = new Point(162, 704);
            buttonSettings.Name = "buttonSettings";
            buttonSettings.Size = new Size(138, 36);
            buttonSettings.TabIndex = 15;
            buttonSettings.Text = "Open settings";
            buttonSettings.UseVisualStyleBackColor = true;
            buttonSettings.Click += buttonSettings_Click;
            // 
            // checkBoxAutoStart
            // 
            checkBoxAutoStart.AutoSize = true;
            checkBoxAutoStart.Location = new Point(331, 708);
            checkBoxAutoStart.Name = "checkBoxAutoStart";
            checkBoxAutoStart.Size = new Size(191, 29);
            checkBoxAutoStart.TabIndex = 16;
            checkBoxAutoStart.Text = "Start with Windows";
            checkBoxAutoStart.UseVisualStyleBackColor = true;
            checkBoxAutoStart.CheckedChanged += checkBoxSettings_CheckedChanged;
            // 
            // checkBoxRestart
            // 
            checkBoxRestart.AutoSize = true;
            checkBoxRestart.Location = new Point(573, 708);
            checkBoxRestart.Name = "checkBoxRestart";
            checkBoxRestart.Size = new Size(194, 29);
            checkBoxRestart.TabIndex = 17;
            checkBoxRestart.Text = "Restart after closing";
            checkBoxRestart.UseVisualStyleBackColor = true;
            checkBoxRestart.CheckedChanged += checkBoxSettings_CheckedChanged;
            // 
            // buttonExit
            // 
            buttonExit.Location = new Point(861, 704);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(125, 36);
            buttonExit.TabIndex = 18;
            buttonExit.Text = "Exit";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // labelStatus
            // 
            labelStatus.AutoEllipsis = true;
            labelStatus.Location = new Point(15, 662);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(970, 30);
            labelStatus.TabIndex = 19;
            labelStatus.Text = "Ready";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 757);
            Controls.Add(listBoxProcesses);
            Controls.Add(buttonUpdate);
            Controls.Add(buttonStop);
            Controls.Add(label1);
            Controls.Add(textBoxInfo);
            Controls.Add(groupBoxBlackList);
            Controls.Add(label2);
            Controls.Add(textBoxInput);
            Controls.Add(buttonStart);
            Controls.Add(buttonLog);
            Controls.Add(buttonSettings);
            Controls.Add(checkBoxAutoStart);
            Controls.Add(checkBoxRestart);
            Controls.Add(buttonExit);
            Controls.Add(labelStatus);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SysProgCS_DZ_09-EXA";
            FormClosing += Form1_FormClosing;
            groupBoxBlackList.ResumeLayout(false);
            groupBoxBlackList.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private ListBox listBoxProcesses = null!;
        private Button buttonUpdate = null!;
        private Button buttonStop = null!;
        private Label label1 = null!;
        private TextBox textBoxInfo = null!;
        private GroupBox groupBoxBlackList = null!;
        private ListBox listBoxBlackList = null!;
        private Button buttonDetection = null!;
        private Button buttonRemove = null!;
        private Button buttonAdd = null!;
        private TextBox textBoxBlackList = null!;
        private Label label2 = null!;
        private TextBox textBoxInput = null!;
        private Button buttonStart = null!;
        private System.Windows.Forms.Timer timerDetection = null!;
        private System.Windows.Forms.Timer timerUpdate = null!;
        private Button buttonLog = null!;
        private Button buttonSettings = null!;
        private CheckBox checkBoxAutoStart = null!;
        private CheckBox checkBoxRestart = null!;
        private Button buttonExit = null!;
        private Label labelStatus = null!;
    }
}
