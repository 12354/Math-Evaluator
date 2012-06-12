namespace MathEvaluator_Grapher
{
    partial class SettingsForm
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.FunctionsTab = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.FunctionListBox = new System.Windows.Forms.ListBox();
            this.checkBoxLocked = new System.Windows.Forms.CheckBox();
            this.checkBoxTransparent = new System.Windows.Forms.CheckBox();
            this.optionsLabel1 = new System.Windows.Forms.Label();
            this.ColorBoxB = new System.Windows.Forms.PictureBox();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.checkBoxDensity = new System.Windows.Forms.CheckBox();
            this.ResetTimeButton = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.timeBox = new System.Windows.Forms.TextBox();
            this.ColorBoxA = new System.Windows.Forms.PictureBox();
            this.ColorLabel = new System.Windows.Forms.Label();
            this.checkBoxVisible = new System.Windows.Forms.CheckBox();
            this.checkBoxImplicit = new System.Windows.Forms.CheckBox();
            this.checkBox3D = new System.Windows.Forms.CheckBox();
            this.ExpressionLabel = new System.Windows.Forms.Label();
            this.ExpressionBox = new System.Windows.Forms.TextBox();
            this.QfiTab = new System.Windows.Forms.TabPage();
            this.QfiBox = new System.Windows.Forms.RichTextBox();
            this.DebugTab = new System.Windows.Forms.TabPage();
            this.checkBoxAA = new System.Windows.Forms.CheckBox();
            this.checkBoxZoom = new System.Windows.Forms.CheckBox();
            this.ExpressionDebug = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.UpdateTime = new System.Windows.Forms.Timer(this.components);
            this.CompileTimer = new System.Windows.Forms.Timer(this.components);
            this.DeriveButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.FunctionsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColorBoxB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorBoxA)).BeginInit();
            this.QfiTab.SuspendLayout();
            this.DebugTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.FunctionsTab);
            this.tabControl1.Controls.Add(this.QfiTab);
            this.tabControl1.Controls.Add(this.DebugTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(364, 320);
            this.tabControl1.TabIndex = 0;
            // 
            // FunctionsTab
            // 
            this.FunctionsTab.Controls.Add(this.splitContainer1);
            this.FunctionsTab.Location = new System.Drawing.Point(4, 22);
            this.FunctionsTab.Name = "FunctionsTab";
            this.FunctionsTab.Size = new System.Drawing.Size(356, 294);
            this.FunctionsTab.TabIndex = 2;
            this.FunctionsTab.Text = "Functions";
            this.FunctionsTab.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FunctionListBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.DeriveButton);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxLocked);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxTransparent);
            this.splitContainer1.Panel2.Controls.Add(this.optionsLabel1);
            this.splitContainer1.Panel2.Controls.Add(this.ColorBoxB);
            this.splitContainer1.Panel2.Controls.Add(this.RemoveButton);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxDensity);
            this.splitContainer1.Panel2.Controls.Add(this.ResetTimeButton);
            this.splitContainer1.Panel2.Controls.Add(this.timeLabel);
            this.splitContainer1.Panel2.Controls.Add(this.timeBox);
            this.splitContainer1.Panel2.Controls.Add(this.ColorBoxA);
            this.splitContainer1.Panel2.Controls.Add(this.ColorLabel);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxVisible);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxImplicit);
            this.splitContainer1.Panel2.Controls.Add(this.checkBox3D);
            this.splitContainer1.Panel2.Controls.Add(this.ExpressionLabel);
            this.splitContainer1.Panel2.Controls.Add(this.ExpressionBox);
            this.splitContainer1.Size = new System.Drawing.Size(356, 294);
            this.splitContainer1.SplitterDistance = 118;
            this.splitContainer1.TabIndex = 0;
            // 
            // FunctionListBox
            // 
            this.FunctionListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FunctionListBox.FormattingEnabled = true;
            this.FunctionListBox.Location = new System.Drawing.Point(0, 0);
            this.FunctionListBox.Name = "FunctionListBox";
            this.FunctionListBox.Size = new System.Drawing.Size(118, 294);
            this.FunctionListBox.TabIndex = 0;
            this.FunctionListBox.SelectedIndexChanged += new System.EventHandler(this.FunctionListBox_SelectedIndexChanged);
            // 
            // checkBoxLocked
            // 
            this.checkBoxLocked.AutoSize = true;
            this.checkBoxLocked.Location = new System.Drawing.Point(143, 120);
            this.checkBoxLocked.Name = "checkBoxLocked";
            this.checkBoxLocked.Size = new System.Drawing.Size(62, 17);
            this.checkBoxLocked.TabIndex = 15;
            this.checkBoxLocked.Text = "Locked";
            this.checkBoxLocked.UseVisualStyleBackColor = true;
            this.checkBoxLocked.CheckedChanged += new System.EventHandler(this.checkBoxLocked_CheckedChanged);
            // 
            // checkBoxTransparent
            // 
            this.checkBoxTransparent.AutoSize = true;
            this.checkBoxTransparent.Location = new System.Drawing.Point(76, 189);
            this.checkBoxTransparent.Name = "checkBoxTransparent";
            this.checkBoxTransparent.Size = new System.Drawing.Size(80, 17);
            this.checkBoxTransparent.TabIndex = 14;
            this.checkBoxTransparent.Text = "Fade Alpha";
            this.checkBoxTransparent.UseVisualStyleBackColor = true;
            this.checkBoxTransparent.CheckedChanged += new System.EventHandler(this.checkBoxTransparent_CheckedChanged);
            // 
            // optionsLabel1
            // 
            this.optionsLabel1.AutoSize = true;
            this.optionsLabel1.Location = new System.Drawing.Point(8, 107);
            this.optionsLabel1.Name = "optionsLabel1";
            this.optionsLabel1.Size = new System.Drawing.Size(46, 13);
            this.optionsLabel1.TabIndex = 13;
            this.optionsLabel1.Text = "Options:";
            // 
            // ColorBoxB
            // 
            this.ColorBoxB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorBoxB.Location = new System.Drawing.Point(142, 36);
            this.ColorBoxB.Name = "ColorBoxB";
            this.ColorBoxB.Size = new System.Drawing.Size(76, 26);
            this.ColorBoxB.TabIndex = 12;
            this.ColorBoxB.TabStop = false;
            this.ColorBoxB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorBoxB_MouseDown);
            this.ColorBoxB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorBoxB_UpdateAlpha);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(151, 263);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveButton.TabIndex = 11;
            this.RemoveButton.Text = "Remove";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // checkBoxDensity
            // 
            this.checkBoxDensity.AutoSize = true;
            this.checkBoxDensity.Location = new System.Drawing.Point(76, 166);
            this.checkBoxDensity.Name = "checkBoxDensity";
            this.checkBoxDensity.Size = new System.Drawing.Size(82, 17);
            this.checkBoxDensity.TabIndex = 10;
            this.checkBoxDensity.Text = "Density Plot";
            this.checkBoxDensity.UseVisualStyleBackColor = true;
            this.checkBoxDensity.CheckedChanged += new System.EventHandler(this.DensityBox_CheckedChanged);
            // 
            // ResetTimeButton
            // 
            this.ResetTimeButton.Location = new System.Drawing.Point(142, 68);
            this.ResetTimeButton.Name = "ResetTimeButton";
            this.ResetTimeButton.Size = new System.Drawing.Size(76, 23);
            this.ResetTimeButton.TabIndex = 9;
            this.ResetTimeButton.Text = "Reset Time";
            this.ResetTimeButton.UseVisualStyleBackColor = true;
            this.ResetTimeButton.Click += new System.EventHandler(this.ResetTimeButton_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(21, 78);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(33, 13);
            this.timeLabel.TabIndex = 8;
            this.timeLabel.Text = "Time:";
            // 
            // timeBox
            // 
            this.timeBox.Location = new System.Drawing.Point(60, 70);
            this.timeBox.Name = "timeBox";
            this.timeBox.ReadOnly = true;
            this.timeBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.timeBox.Size = new System.Drawing.Size(76, 20);
            this.timeBox.TabIndex = 7;
            // 
            // ColorBoxA
            // 
            this.ColorBoxA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorBoxA.Location = new System.Drawing.Point(60, 36);
            this.ColorBoxA.Name = "ColorBoxA";
            this.ColorBoxA.Size = new System.Drawing.Size(76, 26);
            this.ColorBoxA.TabIndex = 6;
            this.ColorBoxA.TabStop = false;
            this.ColorBoxA.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorBoxA_MouseDown);
            this.ColorBoxA.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorBoxA_UpdateAlpha);
            // 
            // ColorLabel
            // 
            this.ColorLabel.AutoSize = true;
            this.ColorLabel.Location = new System.Drawing.Point(20, 49);
            this.ColorLabel.Name = "ColorLabel";
            this.ColorLabel.Size = new System.Drawing.Size(34, 13);
            this.ColorLabel.TabIndex = 5;
            this.ColorLabel.Text = "Color:";
            // 
            // checkBoxVisible
            // 
            this.checkBoxVisible.AutoSize = true;
            this.checkBoxVisible.Location = new System.Drawing.Point(60, 120);
            this.checkBoxVisible.Name = "checkBoxVisible";
            this.checkBoxVisible.Size = new System.Drawing.Size(56, 17);
            this.checkBoxVisible.TabIndex = 4;
            this.checkBoxVisible.Text = "Visible";
            this.checkBoxVisible.UseVisualStyleBackColor = true;
            this.checkBoxVisible.CheckedChanged += new System.EventHandler(this.checkBoxVisible_CheckedChanged);
            // 
            // checkBoxImplicit
            // 
            this.checkBoxImplicit.AutoSize = true;
            this.checkBoxImplicit.Location = new System.Drawing.Point(60, 143);
            this.checkBoxImplicit.Name = "checkBoxImplicit";
            this.checkBoxImplicit.Size = new System.Drawing.Size(58, 17);
            this.checkBoxImplicit.TabIndex = 3;
            this.checkBoxImplicit.Text = "Implicit";
            this.checkBoxImplicit.UseVisualStyleBackColor = true;
            this.checkBoxImplicit.CheckedChanged += new System.EventHandler(this.checkBoxImplicit_CheckedChanged);
            // 
            // checkBox3D
            // 
            this.checkBox3D.AutoSize = true;
            this.checkBox3D.Location = new System.Drawing.Point(143, 143);
            this.checkBox3D.Name = "checkBox3D";
            this.checkBox3D.Size = new System.Drawing.Size(40, 17);
            this.checkBox3D.TabIndex = 2;
            this.checkBox3D.Text = "3D";
            this.checkBox3D.UseVisualStyleBackColor = true;
            this.checkBox3D.CheckedChanged += new System.EventHandler(this.checkBox3D_CheckedChanged);
            // 
            // ExpressionLabel
            // 
            this.ExpressionLabel.AutoSize = true;
            this.ExpressionLabel.Location = new System.Drawing.Point(3, 11);
            this.ExpressionLabel.Name = "ExpressionLabel";
            this.ExpressionLabel.Size = new System.Drawing.Size(51, 13);
            this.ExpressionLabel.TabIndex = 1;
            this.ExpressionLabel.Text = "Function:";
            // 
            // ExpressionBox
            // 
            this.ExpressionBox.Location = new System.Drawing.Point(60, 8);
            this.ExpressionBox.Name = "ExpressionBox";
            this.ExpressionBox.Size = new System.Drawing.Size(157, 20);
            this.ExpressionBox.TabIndex = 0;
            this.ExpressionBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExpressionBox_KeyDown);
            // 
            // QfiTab
            // 
            this.QfiTab.Controls.Add(this.QfiBox);
            this.QfiTab.Location = new System.Drawing.Point(4, 22);
            this.QfiTab.Name = "QfiTab";
            this.QfiTab.Padding = new System.Windows.Forms.Padding(3);
            this.QfiTab.Size = new System.Drawing.Size(356, 294);
            this.QfiTab.TabIndex = 0;
            this.QfiTab.Text = "Qfi";
            this.QfiTab.UseVisualStyleBackColor = true;
            // 
            // QfiBox
            // 
            this.QfiBox.AcceptsTab = true;
            this.QfiBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.QfiBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QfiBox.Location = new System.Drawing.Point(3, 3);
            this.QfiBox.Name = "QfiBox";
            this.QfiBox.Size = new System.Drawing.Size(350, 288);
            this.QfiBox.TabIndex = 0;
            this.QfiBox.Text = "";
            this.QfiBox.WordWrap = false;
            this.QfiBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // DebugTab
            // 
            this.DebugTab.Controls.Add(this.checkBoxAA);
            this.DebugTab.Controls.Add(this.checkBoxZoom);
            this.DebugTab.Controls.Add(this.ExpressionDebug);
            this.DebugTab.Location = new System.Drawing.Point(4, 22);
            this.DebugTab.Name = "DebugTab";
            this.DebugTab.Padding = new System.Windows.Forms.Padding(3);
            this.DebugTab.Size = new System.Drawing.Size(356, 294);
            this.DebugTab.TabIndex = 1;
            this.DebugTab.Text = "Debug";
            this.DebugTab.UseVisualStyleBackColor = true;
            // 
            // checkBoxAA
            // 
            this.checkBoxAA.AutoSize = true;
            this.checkBoxAA.Checked = true;
            this.checkBoxAA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAA.Location = new System.Drawing.Point(25, 91);
            this.checkBoxAA.Name = "checkBoxAA";
            this.checkBoxAA.Size = new System.Drawing.Size(80, 17);
            this.checkBoxAA.TabIndex = 2;
            this.checkBoxAA.Text = "AntiAliasing";
            this.checkBoxAA.UseVisualStyleBackColor = true;
            this.checkBoxAA.CheckedChanged += new System.EventHandler(this.checkBoxAA_CheckedChanged);
            // 
            // checkBoxZoom
            // 
            this.checkBoxZoom.AutoSize = true;
            this.checkBoxZoom.Location = new System.Drawing.Point(25, 68);
            this.checkBoxZoom.Name = "checkBoxZoom";
            this.checkBoxZoom.Size = new System.Drawing.Size(99, 17);
            this.checkBoxZoom.TabIndex = 1;
            this.checkBoxZoom.Text = "Unlimited Zoom";
            this.checkBoxZoom.UseVisualStyleBackColor = true;
            this.checkBoxZoom.CheckedChanged += new System.EventHandler(this.checkBoxZoom_CheckedChanged);
            // 
            // ExpressionDebug
            // 
            this.ExpressionDebug.AutoSize = true;
            this.ExpressionDebug.Location = new System.Drawing.Point(38, 42);
            this.ExpressionDebug.Name = "ExpressionDebug";
            this.ExpressionDebug.Size = new System.Drawing.Size(35, 13);
            this.ExpressionDebug.TabIndex = 0;
            this.ExpressionDebug.Text = "label1";
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // UpdateTime
            // 
            this.UpdateTime.Enabled = true;
            this.UpdateTime.Tick += new System.EventHandler(this.UpdateTime_Tick);
            // 
            // CompileTimer
            // 
            this.CompileTimer.Enabled = true;
            this.CompileTimer.Tick += new System.EventHandler(this.CompileTimer_Tick);
            // 
            // DeriveButton
            // 
            this.DeriveButton.Location = new System.Drawing.Point(70, 263);
            this.DeriveButton.Name = "DeriveButton";
            this.DeriveButton.Size = new System.Drawing.Size(75, 23);
            this.DeriveButton.TabIndex = 16;
            this.DeriveButton.Text = "Derive";
            this.DeriveButton.UseVisualStyleBackColor = true;
            this.DeriveButton.Click += new System.EventHandler(this.DeriveButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 320);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.FunctionsTab.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ColorBoxB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColorBoxA)).EndInit();
            this.QfiTab.ResumeLayout(false);
            this.DebugTab.ResumeLayout(false);
            this.DebugTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage QfiTab;
        private System.Windows.Forms.TabPage DebugTab;
        private System.Windows.Forms.TabPage FunctionsTab;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox FunctionListBox;
        private System.Windows.Forms.Label ExpressionLabel;
        public System.Windows.Forms.TextBox ExpressionBox;
        private System.Windows.Forms.CheckBox checkBoxImplicit;
        private System.Windows.Forms.CheckBox checkBox3D;
        private System.Windows.Forms.CheckBox checkBoxVisible;
        private System.Windows.Forms.PictureBox ColorBoxA;
        private System.Windows.Forms.Label ColorLabel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.TextBox timeBox;
        private System.Windows.Forms.Timer UpdateTime;
        private System.Windows.Forms.Button ResetTimeButton;
        private System.Windows.Forms.CheckBox checkBoxDensity;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.PictureBox ColorBoxB;
        private System.Windows.Forms.CheckBox checkBoxTransparent;
        private System.Windows.Forms.Label optionsLabel1;
        private System.Windows.Forms.CheckBox checkBoxLocked;
        private System.Windows.Forms.RichTextBox QfiBox;
        private System.Windows.Forms.Timer CompileTimer;
        private System.Windows.Forms.Label ExpressionDebug;
        private System.Windows.Forms.CheckBox checkBoxZoom;
        private System.Windows.Forms.CheckBox checkBoxAA;
        private System.Windows.Forms.Button DeriveButton;
    }
}