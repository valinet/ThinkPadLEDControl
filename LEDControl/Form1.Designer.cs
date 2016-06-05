namespace LEDControl
{
    partial class Form1
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
            this.powerOn = new System.Windows.Forms.Button();
            this.powerOff = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.powerBlink = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dotBlink = new System.Windows.Forms.Button();
            this.dotOff = new System.Windows.Forms.Button();
            this.dotOn = new System.Windows.Forms.Button();
            this.microphoneBlink = new System.Windows.Forms.Button();
            this.microphoneOff = new System.Windows.Forms.Button();
            this.microphoneOn = new System.Windows.Forms.Button();
            this.sleepBlink = new System.Windows.Forms.Button();
            this.sleepOff = new System.Windows.Forms.Button();
            this.sleepOn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkHDDReadPower = new System.Windows.Forms.CheckBox();
            this.checkHDDReadDot = new System.Windows.Forms.CheckBox();
            this.checkHDDReadMicrophone = new System.Windows.Forms.CheckBox();
            this.checkHDDReadSleep = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.workerHDD = new System.ComponentModel.BackgroundWorker();
            this.NotifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkHDDWriteSleep = new System.Windows.Forms.CheckBox();
            this.checkHDDWriteMicrophone = new System.Windows.Forms.CheckBox();
            this.checkHDDWriteDot = new System.Windows.Forms.CheckBox();
            this.checkHDDWritePower = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkHDDSleep = new System.Windows.Forms.CheckBox();
            this.checkHDDMicrophone = new System.Windows.Forms.CheckBox();
            this.checkHDDDot = new System.Windows.Forms.CheckBox();
            this.checkHDDPower = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkCLSleep = new System.Windows.Forms.CheckBox();
            this.checkCLMicrophone = new System.Windows.Forms.CheckBox();
            this.checkCLDot = new System.Windows.Forms.CheckBox();
            this.checkCLPower = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.checkHDD = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.checkNLSleep = new System.Windows.Forms.CheckBox();
            this.checkNLMicrophone = new System.Windows.Forms.CheckBox();
            this.checkNLDot = new System.Windows.Forms.CheckBox();
            this.checkNLPower = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.button6 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.rememberKBD = new System.Windows.Forms.CheckBox();
            this.lightTimer = new System.Windows.Forms.Timer(this.components);
            this.checkTurnKBLightOff = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.checkHDDReadFn = new System.Windows.Forms.CheckBox();
            this.checkHDDWriteFn = new System.Windows.Forms.CheckBox();
            this.checkHDDFn = new System.Windows.Forms.CheckBox();
            this.checkCLFn = new System.Windows.Forms.CheckBox();
            this.checkNLFn = new System.Windows.Forms.CheckBox();
            this.checkInvertFn = new System.Windows.Forms.CheckBox();
            this.checkInvertSleep = new System.Windows.Forms.CheckBox();
            this.checkInvertMicrophone = new System.Windows.Forms.CheckBox();
            this.checkInvertDot = new System.Windows.Forms.CheckBox();
            this.checkInvertPower = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // powerOn
            // 
            this.powerOn.Location = new System.Drawing.Point(92, 8);
            this.powerOn.Name = "powerOn";
            this.powerOn.Size = new System.Drawing.Size(75, 23);
            this.powerOn.TabIndex = 0;
            this.powerOn.Text = "ON";
            this.powerOn.UseVisualStyleBackColor = true;
            this.powerOn.Click += new System.EventHandler(this.powerOn_Click);
            // 
            // powerOff
            // 
            this.powerOff.Location = new System.Drawing.Point(173, 8);
            this.powerOff.Name = "powerOff";
            this.powerOff.Size = new System.Drawing.Size(75, 23);
            this.powerOff.TabIndex = 1;
            this.powerOff.Text = "OFF";
            this.powerOff.UseVisualStyleBackColor = true;
            this.powerOff.Click += new System.EventHandler(this.powerOff_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Power button:";
            // 
            // powerBlink
            // 
            this.powerBlink.Location = new System.Drawing.Point(254, 8);
            this.powerBlink.Name = "powerBlink";
            this.powerBlink.Size = new System.Drawing.Size(75, 23);
            this.powerBlink.TabIndex = 3;
            this.powerBlink.Text = "Third state";
            this.powerBlink.UseVisualStyleBackColor = true;
            this.powerBlink.Click += new System.EventHandler(this.powerBlink_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Red Dot:";
            // 
            // dotBlink
            // 
            this.dotBlink.Location = new System.Drawing.Point(254, 37);
            this.dotBlink.Name = "dotBlink";
            this.dotBlink.Size = new System.Drawing.Size(75, 23);
            this.dotBlink.TabIndex = 7;
            this.dotBlink.Text = "Third state";
            this.dotBlink.UseVisualStyleBackColor = true;
            this.dotBlink.Click += new System.EventHandler(this.dotBlink_Click);
            // 
            // dotOff
            // 
            this.dotOff.Location = new System.Drawing.Point(173, 37);
            this.dotOff.Name = "dotOff";
            this.dotOff.Size = new System.Drawing.Size(75, 23);
            this.dotOff.TabIndex = 6;
            this.dotOff.Text = "OFF";
            this.dotOff.UseVisualStyleBackColor = true;
            this.dotOff.Click += new System.EventHandler(this.dotOff_Click);
            // 
            // dotOn
            // 
            this.dotOn.Location = new System.Drawing.Point(92, 37);
            this.dotOn.Name = "dotOn";
            this.dotOn.Size = new System.Drawing.Size(75, 23);
            this.dotOn.TabIndex = 5;
            this.dotOn.Text = "ON";
            this.dotOn.UseVisualStyleBackColor = true;
            this.dotOn.Click += new System.EventHandler(this.dotOn_Click);
            // 
            // microphoneBlink
            // 
            this.microphoneBlink.Location = new System.Drawing.Point(254, 66);
            this.microphoneBlink.Name = "microphoneBlink";
            this.microphoneBlink.Size = new System.Drawing.Size(75, 23);
            this.microphoneBlink.TabIndex = 10;
            this.microphoneBlink.Text = "Third state";
            this.microphoneBlink.UseVisualStyleBackColor = true;
            this.microphoneBlink.Click += new System.EventHandler(this.microphoneBlink_Click);
            // 
            // microphoneOff
            // 
            this.microphoneOff.Location = new System.Drawing.Point(173, 66);
            this.microphoneOff.Name = "microphoneOff";
            this.microphoneOff.Size = new System.Drawing.Size(75, 23);
            this.microphoneOff.TabIndex = 9;
            this.microphoneOff.Text = "OFF";
            this.microphoneOff.UseVisualStyleBackColor = true;
            this.microphoneOff.Click += new System.EventHandler(this.microphoneOff_Click);
            // 
            // microphoneOn
            // 
            this.microphoneOn.Location = new System.Drawing.Point(92, 66);
            this.microphoneOn.Name = "microphoneOn";
            this.microphoneOn.Size = new System.Drawing.Size(75, 23);
            this.microphoneOn.TabIndex = 8;
            this.microphoneOn.Text = "ON";
            this.microphoneOn.UseVisualStyleBackColor = true;
            this.microphoneOn.Click += new System.EventHandler(this.microphoneOn_Click);
            // 
            // sleepBlink
            // 
            this.sleepBlink.Location = new System.Drawing.Point(254, 95);
            this.sleepBlink.Name = "sleepBlink";
            this.sleepBlink.Size = new System.Drawing.Size(75, 23);
            this.sleepBlink.TabIndex = 13;
            this.sleepBlink.Text = "Third state";
            this.sleepBlink.UseVisualStyleBackColor = true;
            this.sleepBlink.Click += new System.EventHandler(this.sleepBlink_Click);
            // 
            // sleepOff
            // 
            this.sleepOff.Location = new System.Drawing.Point(173, 95);
            this.sleepOff.Name = "sleepOff";
            this.sleepOff.Size = new System.Drawing.Size(75, 23);
            this.sleepOff.TabIndex = 12;
            this.sleepOff.Text = "OFF";
            this.sleepOff.UseVisualStyleBackColor = true;
            this.sleepOff.Click += new System.EventHandler(this.sleepOff_Click);
            // 
            // sleepOn
            // 
            this.sleepOn.Location = new System.Drawing.Point(92, 95);
            this.sleepOn.Name = "sleepOn";
            this.sleepOn.Size = new System.Drawing.Size(75, 23);
            this.sleepOn.TabIndex = 11;
            this.sleepOn.Text = "ON";
            this.sleepOn.UseVisualStyleBackColor = true;
            this.sleepOn.Click += new System.EventHandler(this.sleepOn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Microphone:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Sleep moon:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(319, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "____________________________________________________";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Disks read activity:";
            // 
            // checkHDDReadPower
            // 
            this.checkHDDReadPower.AutoSize = true;
            this.checkHDDReadPower.Location = new System.Drawing.Point(16, 190);
            this.checkHDDReadPower.Name = "checkHDDReadPower";
            this.checkHDDReadPower.Size = new System.Drawing.Size(89, 17);
            this.checkHDDReadPower.TabIndex = 18;
            this.checkHDDReadPower.Text = "Power button";
            this.checkHDDReadPower.UseVisualStyleBackColor = true;
            this.checkHDDReadPower.CheckedChanged += new System.EventHandler(this.checkHDDReadPower_CheckedChanged);
            // 
            // checkHDDReadDot
            // 
            this.checkHDDReadDot.AutoSize = true;
            this.checkHDDReadDot.Location = new System.Drawing.Point(16, 213);
            this.checkHDDReadDot.Name = "checkHDDReadDot";
            this.checkHDDReadDot.Size = new System.Drawing.Size(66, 17);
            this.checkHDDReadDot.TabIndex = 19;
            this.checkHDDReadDot.Text = "Red Dot";
            this.checkHDDReadDot.UseVisualStyleBackColor = true;
            this.checkHDDReadDot.CheckedChanged += new System.EventHandler(this.checkHDDReadDot_CheckedChanged);
            // 
            // checkHDDReadMicrophone
            // 
            this.checkHDDReadMicrophone.AutoSize = true;
            this.checkHDDReadMicrophone.Location = new System.Drawing.Point(16, 236);
            this.checkHDDReadMicrophone.Name = "checkHDDReadMicrophone";
            this.checkHDDReadMicrophone.Size = new System.Drawing.Size(82, 17);
            this.checkHDDReadMicrophone.TabIndex = 20;
            this.checkHDDReadMicrophone.Text = "Microphone";
            this.checkHDDReadMicrophone.UseVisualStyleBackColor = true;
            this.checkHDDReadMicrophone.CheckedChanged += new System.EventHandler(this.checkHDDReadMicrophone_CheckedChanged);
            // 
            // checkHDDReadSleep
            // 
            this.checkHDDReadSleep.AutoSize = true;
            this.checkHDDReadSleep.Location = new System.Drawing.Point(16, 259);
            this.checkHDDReadSleep.Name = "checkHDDReadSleep";
            this.checkHDDReadSleep.Size = new System.Drawing.Size(82, 17);
            this.checkHDDReadSleep.TabIndex = 21;
            this.checkHDDReadSleep.Text = "Sleep moon";
            this.checkHDDReadSleep.UseVisualStyleBackColor = true;
            this.checkHDDReadSleep.CheckedChanged += new System.EventHandler(this.checkHDDReadSleep_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 612);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(319, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "____________________________________________________";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 630);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 23;
            this.button1.Text = "About";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // workerHDD
            // 
            this.workerHDD.WorkerSupportsCancellation = true;
            this.workerHDD.DoWork += new System.ComponentModel.DoWorkEventHandler(this.workerHDD_DoWork);
            // 
            // NotifyIcon1
            // 
            this.NotifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.NotifyIcon1.Text = "notifyIcon1";
            this.NotifyIcon1.Visible = true;
            this.NotifyIcon1.DoubleClick += new System.EventHandler(this.NotifyIcon1_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 54);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItem1.Text = "Show application";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(165, 22);
            this.toolStripMenuItem2.Text = "Quit";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // checkHDDWriteSleep
            // 
            this.checkHDDWriteSleep.AutoSize = true;
            this.checkHDDWriteSleep.Location = new System.Drawing.Point(123, 259);
            this.checkHDDWriteSleep.Name = "checkHDDWriteSleep";
            this.checkHDDWriteSleep.Size = new System.Drawing.Size(82, 17);
            this.checkHDDWriteSleep.TabIndex = 28;
            this.checkHDDWriteSleep.Text = "Sleep moon";
            this.checkHDDWriteSleep.UseVisualStyleBackColor = true;
            this.checkHDDWriteSleep.CheckedChanged += new System.EventHandler(this.checkHDDWriteSleep_CheckedChanged);
            // 
            // checkHDDWriteMicrophone
            // 
            this.checkHDDWriteMicrophone.AutoSize = true;
            this.checkHDDWriteMicrophone.Location = new System.Drawing.Point(123, 236);
            this.checkHDDWriteMicrophone.Name = "checkHDDWriteMicrophone";
            this.checkHDDWriteMicrophone.Size = new System.Drawing.Size(82, 17);
            this.checkHDDWriteMicrophone.TabIndex = 27;
            this.checkHDDWriteMicrophone.Text = "Microphone";
            this.checkHDDWriteMicrophone.UseVisualStyleBackColor = true;
            this.checkHDDWriteMicrophone.CheckedChanged += new System.EventHandler(this.checkHDDWriteMicrophone_CheckedChanged);
            // 
            // checkHDDWriteDot
            // 
            this.checkHDDWriteDot.AutoSize = true;
            this.checkHDDWriteDot.Location = new System.Drawing.Point(123, 213);
            this.checkHDDWriteDot.Name = "checkHDDWriteDot";
            this.checkHDDWriteDot.Size = new System.Drawing.Size(66, 17);
            this.checkHDDWriteDot.TabIndex = 26;
            this.checkHDDWriteDot.Text = "Red Dot";
            this.checkHDDWriteDot.UseVisualStyleBackColor = true;
            this.checkHDDWriteDot.CheckedChanged += new System.EventHandler(this.checkHDDWriteDot_CheckedChanged);
            // 
            // checkHDDWritePower
            // 
            this.checkHDDWritePower.AutoSize = true;
            this.checkHDDWritePower.Location = new System.Drawing.Point(123, 190);
            this.checkHDDWritePower.Name = "checkHDDWritePower";
            this.checkHDDWritePower.Size = new System.Drawing.Size(89, 17);
            this.checkHDDWritePower.TabIndex = 25;
            this.checkHDDWritePower.Text = "Power button";
            this.checkHDDWritePower.UseVisualStyleBackColor = true;
            this.checkHDDWritePower.CheckedChanged += new System.EventHandler(this.checkHDDWritePower_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(120, 170);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Disks write activity:";
            // 
            // checkHDDSleep
            // 
            this.checkHDDSleep.AutoSize = true;
            this.checkHDDSleep.Enabled = false;
            this.checkHDDSleep.Location = new System.Drawing.Point(235, 259);
            this.checkHDDSleep.Name = "checkHDDSleep";
            this.checkHDDSleep.Size = new System.Drawing.Size(82, 17);
            this.checkHDDSleep.TabIndex = 33;
            this.checkHDDSleep.Text = "Sleep moon";
            this.checkHDDSleep.UseVisualStyleBackColor = true;
            // 
            // checkHDDMicrophone
            // 
            this.checkHDDMicrophone.AutoSize = true;
            this.checkHDDMicrophone.Enabled = false;
            this.checkHDDMicrophone.Location = new System.Drawing.Point(235, 236);
            this.checkHDDMicrophone.Name = "checkHDDMicrophone";
            this.checkHDDMicrophone.Size = new System.Drawing.Size(82, 17);
            this.checkHDDMicrophone.TabIndex = 32;
            this.checkHDDMicrophone.Text = "Microphone";
            this.checkHDDMicrophone.UseVisualStyleBackColor = true;
            // 
            // checkHDDDot
            // 
            this.checkHDDDot.AutoSize = true;
            this.checkHDDDot.Enabled = false;
            this.checkHDDDot.Location = new System.Drawing.Point(235, 213);
            this.checkHDDDot.Name = "checkHDDDot";
            this.checkHDDDot.Size = new System.Drawing.Size(66, 17);
            this.checkHDDDot.TabIndex = 31;
            this.checkHDDDot.Text = "Red Dot";
            this.checkHDDDot.UseVisualStyleBackColor = true;
            // 
            // checkHDDPower
            // 
            this.checkHDDPower.AutoSize = true;
            this.checkHDDPower.Enabled = false;
            this.checkHDDPower.Location = new System.Drawing.Point(235, 190);
            this.checkHDDPower.Name = "checkHDDPower";
            this.checkHDDPower.Size = new System.Drawing.Size(89, 17);
            this.checkHDDPower.TabIndex = 30;
            this.checkHDDPower.Text = "Power button";
            this.checkHDDPower.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(232, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Disks activity:";
            // 
            // checkCLSleep
            // 
            this.checkCLSleep.AutoSize = true;
            this.checkCLSleep.Location = new System.Drawing.Point(16, 451);
            this.checkCLSleep.Name = "checkCLSleep";
            this.checkCLSleep.Size = new System.Drawing.Size(82, 17);
            this.checkCLSleep.TabIndex = 38;
            this.checkCLSleep.Text = "Sleep moon";
            this.checkCLSleep.UseVisualStyleBackColor = true;
            // 
            // checkCLMicrophone
            // 
            this.checkCLMicrophone.AutoSize = true;
            this.checkCLMicrophone.Location = new System.Drawing.Point(16, 427);
            this.checkCLMicrophone.Name = "checkCLMicrophone";
            this.checkCLMicrophone.Size = new System.Drawing.Size(82, 17);
            this.checkCLMicrophone.TabIndex = 37;
            this.checkCLMicrophone.Text = "Microphone";
            this.checkCLMicrophone.UseVisualStyleBackColor = true;
            // 
            // checkCLDot
            // 
            this.checkCLDot.AutoSize = true;
            this.checkCLDot.Location = new System.Drawing.Point(16, 405);
            this.checkCLDot.Name = "checkCLDot";
            this.checkCLDot.Size = new System.Drawing.Size(66, 17);
            this.checkCLDot.TabIndex = 36;
            this.checkCLDot.Text = "Red Dot";
            this.checkCLDot.UseVisualStyleBackColor = true;
            // 
            // checkCLPower
            // 
            this.checkCLPower.AutoSize = true;
            this.checkCLPower.Location = new System.Drawing.Point(16, 382);
            this.checkCLPower.Name = "checkCLPower";
            this.checkCLPower.Size = new System.Drawing.Size(89, 17);
            this.checkCLPower.TabIndex = 35;
            this.checkCLPower.Text = "Power button";
            this.checkCLPower.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 362);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Caps Lock status:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 526);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 39;
            this.label11.Text = "Delay:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(47, 525);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown1.TabIndex = 40;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            this.numericUpDown1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown1_KeyPress);
            // 
            // checkHDD
            // 
            this.checkHDD.AutoSize = true;
            this.checkHDD.Location = new System.Drawing.Point(16, 306);
            this.checkHDD.Name = "checkHDD";
            this.checkHDD.Size = new System.Drawing.Size(201, 17);
            this.checkHDD.TabIndex = 41;
            this.checkHDD.Text = "Disable disk drives activity monitoring";
            this.checkHDD.UseVisualStyleBackColor = true;
            this.checkHDD.CheckedChanged += new System.EventHandler(this.checkHDD_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(223, 302);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(25, 23);
            this.button2.TabIndex = 42;
            this.button2.Text = "?";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(104, 522);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(25, 23);
            this.button3.TabIndex = 43;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(92, 630);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(237, 23);
            this.button4.TabIndex = 44;
            this.button4.Text = "Register to run at system startup as admin";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(104, 330);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(25, 23);
            this.button5.TabIndex = 47;
            this.button5.Text = "?";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(47, 332);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown2.TabIndex = 46;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 334);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 45;
            this.label12.Text = "Delay:";
            // 
            // checkNLSleep
            // 
            this.checkNLSleep.AutoSize = true;
            this.checkNLSleep.Location = new System.Drawing.Point(123, 451);
            this.checkNLSleep.Name = "checkNLSleep";
            this.checkNLSleep.Size = new System.Drawing.Size(82, 17);
            this.checkNLSleep.TabIndex = 52;
            this.checkNLSleep.Text = "Sleep moon";
            this.checkNLSleep.UseVisualStyleBackColor = true;
            // 
            // checkNLMicrophone
            // 
            this.checkNLMicrophone.AutoSize = true;
            this.checkNLMicrophone.Location = new System.Drawing.Point(123, 427);
            this.checkNLMicrophone.Name = "checkNLMicrophone";
            this.checkNLMicrophone.Size = new System.Drawing.Size(82, 17);
            this.checkNLMicrophone.TabIndex = 51;
            this.checkNLMicrophone.Text = "Microphone";
            this.checkNLMicrophone.UseVisualStyleBackColor = true;
            // 
            // checkNLDot
            // 
            this.checkNLDot.AutoSize = true;
            this.checkNLDot.Location = new System.Drawing.Point(123, 405);
            this.checkNLDot.Name = "checkNLDot";
            this.checkNLDot.Size = new System.Drawing.Size(66, 17);
            this.checkNLDot.TabIndex = 50;
            this.checkNLDot.Text = "Red Dot";
            this.checkNLDot.UseVisualStyleBackColor = true;
            // 
            // checkNLPower
            // 
            this.checkNLPower.AutoSize = true;
            this.checkNLPower.Location = new System.Drawing.Point(123, 382);
            this.checkNLPower.Name = "checkNLPower";
            this.checkNLPower.Size = new System.Drawing.Size(89, 17);
            this.checkNLPower.TabIndex = 49;
            this.checkNLPower.Text = "Power button";
            this.checkNLPower.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(120, 362);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 13);
            this.label13.TabIndex = 48;
            this.label13.Text = "NumLock status:";
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(16, 500);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(137, 17);
            this.checkBox5.TabIndex = 53;
            this.checkBox5.Text = "Disable keys monitoring";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(159, 496);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(25, 23);
            this.button6.TabIndex = 54;
            this.button6.Text = "?";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(12, 660);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(161, 23);
            this.button7.TabIndex = 55;
            this.button7.Text = "Debug LEDs (advanced)";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(173, 660);
            this.button8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(56, 19);
            this.button8.TabIndex = 56;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Visible = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // rememberKBD
            // 
            this.rememberKBD.Location = new System.Drawing.Point(16, 552);
            this.rememberKBD.Name = "rememberKBD";
            this.rememberKBD.Size = new System.Drawing.Size(327, 32);
            this.rememberKBD.TabIndex = 57;
            this.rememberKBD.Text = "Remember keyboard illumination level after restart, sleep, and other power events" +
    "";
            this.rememberKBD.UseVisualStyleBackColor = true;
            this.rememberKBD.CheckedChanged += new System.EventHandler(this.rememberKBD_CheckedChanged);
            // 
            // lightTimer
            // 
            this.lightTimer.Interval = 1000;
            this.lightTimer.Tick += new System.EventHandler(this.lightTimer_Tick);
            // 
            // checkTurnKBLightOff
            // 
            this.checkTurnKBLightOff.Location = new System.Drawing.Point(16, 591);
            this.checkTurnKBLightOff.Name = "checkTurnKBLightOff";
            this.checkTurnKBLightOff.Size = new System.Drawing.Size(327, 32);
            this.checkTurnKBLightOff.TabIndex = 58;
            this.checkTurnKBLightOff.Text = "Turn keyboard light off when applications go full screen (ideal for watching movi" +
    "es)";
            this.checkTurnKBLightOff.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 129);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 13);
            this.label14.TabIndex = 62;
            this.label14.Text = "Fn Lock:";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(254, 124);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 61;
            this.button9.Text = "Third state";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(173, 124);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 60;
            this.button10.Text = "OFF";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(92, 124);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 59;
            this.button11.Text = "ON";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // checkHDDReadFn
            // 
            this.checkHDDReadFn.AutoSize = true;
            this.checkHDDReadFn.Location = new System.Drawing.Point(16, 283);
            this.checkHDDReadFn.Name = "checkHDDReadFn";
            this.checkHDDReadFn.Size = new System.Drawing.Size(65, 17);
            this.checkHDDReadFn.TabIndex = 63;
            this.checkHDDReadFn.Text = "Fn Lock";
            this.checkHDDReadFn.UseVisualStyleBackColor = true;
            this.checkHDDReadFn.CheckedChanged += new System.EventHandler(this.checkHDDReadFn_CheckedChanged);
            // 
            // checkHDDWriteFn
            // 
            this.checkHDDWriteFn.AutoSize = true;
            this.checkHDDWriteFn.Location = new System.Drawing.Point(123, 283);
            this.checkHDDWriteFn.Name = "checkHDDWriteFn";
            this.checkHDDWriteFn.Size = new System.Drawing.Size(65, 17);
            this.checkHDDWriteFn.TabIndex = 64;
            this.checkHDDWriteFn.Text = "Fn Lock";
            this.checkHDDWriteFn.UseVisualStyleBackColor = true;
            this.checkHDDWriteFn.CheckedChanged += new System.EventHandler(this.checkHDDWriteFn_CheckedChanged);
            // 
            // checkHDDFn
            // 
            this.checkHDDFn.AutoSize = true;
            this.checkHDDFn.Enabled = false;
            this.checkHDDFn.Location = new System.Drawing.Point(235, 283);
            this.checkHDDFn.Name = "checkHDDFn";
            this.checkHDDFn.Size = new System.Drawing.Size(65, 17);
            this.checkHDDFn.TabIndex = 65;
            this.checkHDDFn.Text = "Fn Lock";
            this.checkHDDFn.UseVisualStyleBackColor = true;
            // 
            // checkCLFn
            // 
            this.checkCLFn.AutoSize = true;
            this.checkCLFn.Location = new System.Drawing.Point(16, 475);
            this.checkCLFn.Name = "checkCLFn";
            this.checkCLFn.Size = new System.Drawing.Size(65, 17);
            this.checkCLFn.TabIndex = 66;
            this.checkCLFn.Text = "Fn Lock";
            this.checkCLFn.UseVisualStyleBackColor = true;
            // 
            // checkNLFn
            // 
            this.checkNLFn.AutoSize = true;
            this.checkNLFn.Location = new System.Drawing.Point(123, 474);
            this.checkNLFn.Name = "checkNLFn";
            this.checkNLFn.Size = new System.Drawing.Size(65, 17);
            this.checkNLFn.TabIndex = 67;
            this.checkNLFn.Text = "Fn Lock";
            this.checkNLFn.UseVisualStyleBackColor = true;
            // 
            // checkInvertFn
            // 
            this.checkInvertFn.AutoSize = true;
            this.checkInvertFn.Location = new System.Drawing.Point(235, 474);
            this.checkInvertFn.Name = "checkInvertFn";
            this.checkInvertFn.Size = new System.Drawing.Size(65, 17);
            this.checkInvertFn.TabIndex = 73;
            this.checkInvertFn.Text = "Fn Lock";
            this.checkInvertFn.UseVisualStyleBackColor = true;
            // 
            // checkInvertSleep
            // 
            this.checkInvertSleep.AutoSize = true;
            this.checkInvertSleep.Location = new System.Drawing.Point(235, 451);
            this.checkInvertSleep.Name = "checkInvertSleep";
            this.checkInvertSleep.Size = new System.Drawing.Size(82, 17);
            this.checkInvertSleep.TabIndex = 72;
            this.checkInvertSleep.Text = "Sleep moon";
            this.checkInvertSleep.UseVisualStyleBackColor = true;
            // 
            // checkInvertMicrophone
            // 
            this.checkInvertMicrophone.AutoSize = true;
            this.checkInvertMicrophone.Location = new System.Drawing.Point(235, 427);
            this.checkInvertMicrophone.Name = "checkInvertMicrophone";
            this.checkInvertMicrophone.Size = new System.Drawing.Size(82, 17);
            this.checkInvertMicrophone.TabIndex = 71;
            this.checkInvertMicrophone.Text = "Microphone";
            this.checkInvertMicrophone.UseVisualStyleBackColor = true;
            // 
            // checkInvertDot
            // 
            this.checkInvertDot.AutoSize = true;
            this.checkInvertDot.Location = new System.Drawing.Point(235, 405);
            this.checkInvertDot.Name = "checkInvertDot";
            this.checkInvertDot.Size = new System.Drawing.Size(66, 17);
            this.checkInvertDot.TabIndex = 70;
            this.checkInvertDot.Text = "Red Dot";
            this.checkInvertDot.UseVisualStyleBackColor = true;
            // 
            // checkInvertPower
            // 
            this.checkInvertPower.AutoSize = true;
            this.checkInvertPower.Location = new System.Drawing.Point(235, 382);
            this.checkInvertPower.Name = "checkInvertPower";
            this.checkInvertPower.Size = new System.Drawing.Size(89, 17);
            this.checkInvertPower.TabIndex = 69;
            this.checkInvertPower.Text = "Power button";
            this.checkInvertPower.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(232, 362);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 13);
            this.label15.TabIndex = 68;
            this.label15.Text = "Invert action:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 693);
            this.Controls.Add(this.checkInvertFn);
            this.Controls.Add(this.checkInvertSleep);
            this.Controls.Add(this.checkInvertMicrophone);
            this.Controls.Add(this.checkInvertDot);
            this.Controls.Add(this.checkInvertPower);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.checkNLFn);
            this.Controls.Add(this.checkCLFn);
            this.Controls.Add(this.checkHDDFn);
            this.Controls.Add(this.checkHDDWriteFn);
            this.Controls.Add(this.checkHDDReadFn);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.checkTurnKBLightOff);
            this.Controls.Add(this.rememberKBD);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.checkNLSleep);
            this.Controls.Add(this.checkNLMicrophone);
            this.Controls.Add(this.checkNLDot);
            this.Controls.Add(this.checkNLPower);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkHDD);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.checkCLSleep);
            this.Controls.Add(this.checkCLMicrophone);
            this.Controls.Add(this.checkCLDot);
            this.Controls.Add(this.checkCLPower);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.checkHDDSleep);
            this.Controls.Add(this.checkHDDMicrophone);
            this.Controls.Add(this.checkHDDDot);
            this.Controls.Add(this.checkHDDPower);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.checkHDDWriteSleep);
            this.Controls.Add(this.checkHDDWriteMicrophone);
            this.Controls.Add(this.checkHDDWriteDot);
            this.Controls.Add(this.checkHDDWritePower);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.checkHDDReadSleep);
            this.Controls.Add(this.checkHDDReadMicrophone);
            this.Controls.Add(this.checkHDDReadDot);
            this.Controls.Add(this.checkHDDReadPower);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sleepBlink);
            this.Controls.Add(this.sleepOff);
            this.Controls.Add(this.sleepOn);
            this.Controls.Add(this.microphoneBlink);
            this.Controls.Add(this.microphoneOff);
            this.Controls.Add(this.microphoneOn);
            this.Controls.Add(this.dotBlink);
            this.Controls.Add(this.dotOff);
            this.Controls.Add(this.dotOn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.powerBlink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.powerOff);
            this.Controls.Add(this.powerOn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ThinkPad LEDs Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button powerOn;
        private System.Windows.Forms.Button powerOff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button powerBlink;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button dotBlink;
        private System.Windows.Forms.Button dotOff;
        private System.Windows.Forms.Button dotOn;
        private System.Windows.Forms.Button microphoneBlink;
        private System.Windows.Forms.Button microphoneOff;
        private System.Windows.Forms.Button microphoneOn;
        private System.Windows.Forms.Button sleepBlink;
        private System.Windows.Forms.Button sleepOff;
        private System.Windows.Forms.Button sleepOn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkHDDReadPower;
        private System.Windows.Forms.CheckBox checkHDDReadDot;
        private System.Windows.Forms.CheckBox checkHDDReadMicrophone;
        private System.Windows.Forms.CheckBox checkHDDReadSleep;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker workerHDD;
        private System.Windows.Forms.NotifyIcon NotifyIcon1;
        private System.Windows.Forms.CheckBox checkHDDWriteSleep;
        private System.Windows.Forms.CheckBox checkHDDWriteMicrophone;
        private System.Windows.Forms.CheckBox checkHDDWriteDot;
        private System.Windows.Forms.CheckBox checkHDDWritePower;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkHDDSleep;
        private System.Windows.Forms.CheckBox checkHDDMicrophone;
        private System.Windows.Forms.CheckBox checkHDDDot;
        private System.Windows.Forms.CheckBox checkHDDPower;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.CheckBox checkCLSleep;
        private System.Windows.Forms.CheckBox checkCLMicrophone;
        private System.Windows.Forms.CheckBox checkCLDot;
        private System.Windows.Forms.CheckBox checkCLPower;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox checkHDD;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox checkNLSleep;
        private System.Windows.Forms.CheckBox checkNLMicrophone;
        private System.Windows.Forms.CheckBox checkNLDot;
        private System.Windows.Forms.CheckBox checkNLPower;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.CheckBox rememberKBD;
        private System.Windows.Forms.Timer lightTimer;
        private System.Windows.Forms.CheckBox checkTurnKBLightOff;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.CheckBox checkHDDReadFn;
        private System.Windows.Forms.CheckBox checkHDDWriteFn;
        private System.Windows.Forms.CheckBox checkHDDFn;
        private System.Windows.Forms.CheckBox checkCLFn;
        private System.Windows.Forms.CheckBox checkNLFn;
        private System.Windows.Forms.CheckBox checkInvertFn;
        private System.Windows.Forms.CheckBox checkInvertSleep;
        private System.Windows.Forms.CheckBox checkInvertMicrophone;
        private System.Windows.Forms.CheckBox checkInvertDot;
        private System.Windows.Forms.CheckBox checkInvertPower;
        private System.Windows.Forms.Label label15;
    }
}

