using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.ApplicationServices;
using CoreAudioApi;

namespace LEDControl
{
    public partial class Form1 : Form
    {

        #region constants
        const int EC_DATAPORT = 0x62;
        const int EC_CTRLPORT = 0x66;
        const int EC_STAT_OBF = 0x01;    // Output buffer full 
        const int EC_STAT_IBF = 0x02;    // Input buffer full 
        const int EC_STAT_CMD = 0x08;
        const byte EC_CTRLPORT_READ = 0x80;
        const byte EC_CTRLPORT_WRITE = 0x81;
        const byte EC_CTRLPORT_QUERY = 0x84;
        const byte TP_ECOFFSET_FAN = 0x2F;  // 1 byte (binary xyzz zzz)
        const byte TP_ECOFFSET_FANSPEED = 0x84; // 16 bit word, lo/hi byte
        const int TP_ECOFFSET_TEMP0 = 0x78;    // 8 temp sensor bytes from here
        const int TP_ECOFFSET_TEMP1 = 0xC0; // 4 temp sensor bytes from here

        const byte TP_LED_OFFSET = 0x0C;
        #endregion

        #region EC_access_methods
        bool waitportstatus(int bits, bool onoff = false, int timeout = 1000)
        {
            ushort port = EC_CTRLPORT;
            int time = 0;
            int tick = 10;
            //
            // wait until input on control port has desired state or times out
            //
            for (time = 0; time < timeout; time += tick)
            {
                byte data = 0;
                try
                {
                    if (Properties.Settings.Default.Driver == 0)
                        data = ols.ReadIoPortByte(port);
                    else if (Properties.Settings.Default.Driver == 1)
                        data = TVicPort.ReadPort(port);
                }
                catch
                {
                    return false;
                }

                // check for desired result
                bool flagstate = (((char)data) & bits) != 0,
                    wantedstate = onoff != false;

                if (flagstate == wantedstate)
                {
                    break;
                }

                // try again after a moment
                System.Threading.Thread.Sleep(tick);
            }
            return true;
        }

        bool writeport(ushort port, byte data)
        {
            // write byte via WINIO.SYS
            try
            {
                if (Properties.Settings.Default.Driver == 0)
                    ols.WriteIoPortByte(port, data);
                else if (Properties.Settings.Default.Driver == 1)
                    TVicPort.WritePort(port, data);
            }
            catch
            {
                return false;
            }
            return true;
        }

        bool readport(ushort port, ref byte pdata)
        {
            byte data = 0;
            try
            {
                if (Properties.Settings.Default.Driver == 0)
                    data = ols.ReadIoPortByte(port);
                else if (Properties.Settings.Default.Driver == 1)
                    data = TVicPort.ReadPort(port);
                pdata = data;
            }
            catch
            {
                return false;
            }
            return true;
        }

        bool ReadByteFromEC(byte offset, ref byte pdata)
        {
            bool ok;

            // wait for IBF and OBF to clear
            ok = waitportstatus(EC_STAT_IBF | EC_STAT_OBF, false);
            if (ok)
            {

                // tell 'em we want to "READ"
                ok = writeport(EC_CTRLPORT, EC_CTRLPORT_READ);
                if (ok)
                {

                    // wait for IBF to clear (command byte removed from EC's input queue)
                    ok = waitportstatus(EC_STAT_IBF, false);
                    if (ok)
                    {

                        // tell 'em where we want to read from
                        ok = writeport(EC_DATAPORT, offset);
                        if (ok)
                        {

                            // wait for IBF to clear (address byte removed from EC's input queue)
                            // Note: Techically we should waitportstatus(OBF,TRUE) here,(a byte being 
                            //       in the EC's output buffer being ready to read).  For some reason
                            //       this never seems to happen
                            ok = waitportstatus(EC_STAT_IBF, false);
                            if (ok)
                            {
                                byte data = 0xFF;

                                // read result (EC byte at offset)
                                ok = readport(EC_DATAPORT, ref data);
                                if (ok)
                                    pdata = data;
                            }
                        }
                    }
                }
            }
            return ok;
        }

        bool WriteByteToEC(byte offset, byte data)
        {
            bool ok;

            // wait for IBF and OBF to clear
            ok = waitportstatus(EC_STAT_IBF | EC_STAT_OBF, false);
            if (ok)
            {

                // tell 'em we want to "WRITE"
                ok = writeport(EC_CTRLPORT, EC_CTRLPORT_WRITE);
                if (ok)
                {

                    // wait for IBF to clear (command byte removed from EC's input queue)
                    ok = waitportstatus(EC_STAT_IBF, false);
                    if (ok)
                    {

                        // tell 'em where we want to write to
                        ok = writeport(EC_DATAPORT, offset);
                        if (ok)
                        {

                            // wait for IBF to clear (address byte removed from EC's input queue)
                            ok = waitportstatus(EC_STAT_IBF, false);
                            if (ok)
                            {
                                // tell 'em what we want to write there
                                ok = writeport(EC_DATAPORT, data);
                                if (ok)
                                {
                                    // wait for IBF to clear (data byte removed from EC's input queue)
                                    ok = waitportstatus(EC_STAT_IBF, false);
                                }
                            }
                        }
                    }
                }
            }
            return ok;
        }
        #endregion

        #region ToggleLEDs
        public enum LEDs
        {
            Power, Microphone, RedDot, Sleep, Fn
        }
        public enum PowerStates
        {
            On, Off, Blink
        }
        bool LED(LEDs which, PowerStates what, bool mayOverride = true)
        {
            byte led = 0xFF;
            byte power = 0xFF;
            switch (which)
            {
                case LEDs.Power:
                    led = 0x00;
                    if (checkInvertPower.Checked && mayOverride)
                    {
                        if (what == PowerStates.On) what = PowerStates.Off;
                        else if (what == PowerStates.Off) what = PowerStates.On;
                    }
                    break;
                case LEDs.Microphone:
                    led = 0x0E;
                    if (checkInvertMicrophone.Checked && mayOverride)
                    {
                        if (what == PowerStates.On) what = PowerStates.Off;
                        else if (what == PowerStates.Off) what = PowerStates.On;
                    }
                    break;
                case LEDs.RedDot:
                    led = 0x0A;
                    if (checkInvertDot.Checked && mayOverride)
                    {
                        if (what == PowerStates.On) what = PowerStates.Off;
                        else if (what == PowerStates.Off) what = PowerStates.On;
                    }
                    break;
                case LEDs.Sleep:
                    led = 0x07;
                    if (checkInvertSleep.Checked && mayOverride)
                    {
                        if (what == PowerStates.On) what = PowerStates.Off;
                        else if (what == PowerStates.Off) what = PowerStates.On;
                    }
                    break;
                case LEDs.Fn:
                    led = 0x06;
                    if (checkInvertFn.Checked && mayOverride)
                    {
                        if (what == PowerStates.On) what = PowerStates.Off;
                        else if (what == PowerStates.Off) what = PowerStates.On;
                    }
                    break;
            }
            switch (what)
            {
                case PowerStates.On:
                    power = 0x80;
                    break;
                case PowerStates.Off:
                    power = 0x00;
                    break;
                case PowerStates.Blink:
                    power = 0xC0;
                    break;
            }
            byte _out = (byte)(led | power);
            return WriteByteToEC(TP_LED_OFFSET, _out);
        }
        #endregion

        #region Settings
        void ReadSettingsKBD()
        {
            if (rememberKBD.Checked)
            {
                switch (Properties.Settings.Default.KBDLevel)
                {
                    case 0:
                        SetKeyboardLevel(LightLevel.Off);
                        break;
                    case 1:
                        SetKeyboardLevel(LightLevel.Low);
                        break;
                    case 2:
                        SetKeyboardLevel(LightLevel.High);
                        break;
                }
            }

        }
        void SaveSettingsKBD()
        {
            if (rememberKBD.Checked)
            {
                LightLevel p = GetKeyboardLightlevel();
                Console.WriteLine(p);
                switch (p)
                {
                    case LightLevel.Off:
                        Properties.Settings.Default.KBDLevel = 0;
                        break;
                    case LightLevel.Low:
                        Properties.Settings.Default.KBDLevel = 1;
                        break;
                    case LightLevel.High:
                        Properties.Settings.Default.KBDLevel = 2;
                        break;
                    case LightLevel.Unknown:
                        Properties.Settings.Default.KBDLevel = -1;
                        break;
                }
            }
            Properties.Settings.Default.Save();
        }
        void ReadSettings()
        {
            checkHDDReadDot.Checked = Properties.Settings.Default.HDDReadDot;
            checkHDDReadPower.Checked = Properties.Settings.Default.HDDReadPower;
            checkHDDReadMicrophone.Checked = Properties.Settings.Default.HDDReadMicrophone;
            checkHDDReadSleep.Checked = Properties.Settings.Default.HDDReadSleep;
            checkHDDReadFn.Checked = Properties.Settings.Default.HDDReadFn;

            checkHDDWriteDot.Checked = Properties.Settings.Default.HDDWriteDot;
            checkHDDWritePower.Checked = Properties.Settings.Default.HDDWritePower;
            checkHDDWriteMicrophone.Checked = Properties.Settings.Default.HDDWriteMicrophone;
            checkHDDWriteSleep.Checked = Properties.Settings.Default.HDDWriteSleep;
            checkHDDWriteFn.Checked = Properties.Settings.Default.HDDWriteFn;

            checkCLDot.Checked = Properties.Settings.Default.CLDot;
            checkCLMicrophone.Checked = Properties.Settings.Default.CLMicrophone;
            checkCLPower.Checked = Properties.Settings.Default.CLPower;
            checkCLSleep.Checked = Properties.Settings.Default.CLSleep;
            checkCLFn.Checked = Properties.Settings.Default.CLFn;

            checkNLDot.Checked = Properties.Settings.Default.NLDot;
            checkNLMicrophone.Checked = Properties.Settings.Default.NLMicrophone;
            checkNLPower.Checked = Properties.Settings.Default.NLPower;
            checkNLSleep.Checked = Properties.Settings.Default.NLSleep;
            checkNLFn.Checked = Properties.Settings.Default.NLFn;

            checkInvertDot.Checked = Properties.Settings.Default.InvertDot;
            checkInvertMicrophone.Checked = Properties.Settings.Default.InvertMicrophone;
            checkInvertPower.Checked = Properties.Settings.Default.InvertPower;
            checkInvertSleep.Checked = Properties.Settings.Default.InvertSleep;
            checkInvertFn.Checked = Properties.Settings.Default.InvertFn;


            numericUpDown1.Value = Properties.Settings.Default.CapsLockDelay;
            numericUpDown2.Value = Properties.Settings.Default.HDDDelay;

            checkHDD.Checked = Properties.Settings.Default.HDDDisable;

            rememberKBD.Checked = Properties.Settings.Default.RememberKBD;

            checkTurnKBLightOff.Checked = Properties.Settings.Default.LightOffWhileFS;

        }

        void SaveSettings()
        {
            Properties.Settings.Default.HDDReadDot = checkHDDReadDot.Checked;
            Properties.Settings.Default.HDDReadPower = checkHDDReadPower.Checked;
            Properties.Settings.Default.HDDReadMicrophone = checkHDDReadMicrophone.Checked;
            Properties.Settings.Default.HDDReadSleep = checkHDDReadSleep.Checked;
            Properties.Settings.Default.HDDReadFn = checkHDDReadFn.Checked;

            Properties.Settings.Default.HDDWriteDot = checkHDDWriteDot.Checked;
            Properties.Settings.Default.HDDWritePower = checkHDDWritePower.Checked;
            Properties.Settings.Default.HDDWriteMicrophone = checkHDDWriteMicrophone.Checked;
            Properties.Settings.Default.HDDWriteSleep = checkHDDWriteSleep.Checked;
            Properties.Settings.Default.HDDWriteFn = checkHDDWriteFn.Checked;

            Properties.Settings.Default.CLDot = checkCLDot.Checked;
            Properties.Settings.Default.CLMicrophone = checkCLMicrophone.Checked;
            Properties.Settings.Default.CLPower = checkCLPower.Checked;
            Properties.Settings.Default.CLSleep = checkCLSleep.Checked;
            Properties.Settings.Default.CLFn = checkCLFn.Checked;

            Properties.Settings.Default.NLDot = checkNLDot.Checked;
            Properties.Settings.Default.NLMicrophone = checkNLMicrophone.Checked;
            Properties.Settings.Default.NLPower = checkNLPower.Checked;
            Properties.Settings.Default.NLSleep = checkNLSleep.Checked;
            Properties.Settings.Default.NLFn = checkNLFn.Checked;

            Properties.Settings.Default.InvertDot = checkInvertDot.Checked;
            Properties.Settings.Default.InvertMicrophone = checkInvertMicrophone.Checked;
            Properties.Settings.Default.InvertPower = checkInvertPower.Checked;
            Properties.Settings.Default.InvertSleep = checkInvertSleep.Checked;
            Properties.Settings.Default.InvertFn = checkInvertFn.Checked;

            Properties.Settings.Default.CapsLockDelay = (int)numericUpDown1.Value;
            Properties.Settings.Default.HDDDelay = (int)numericUpDown2.Value;

            Properties.Settings.Default.HDDDisable = checkHDD.Checked;

            Properties.Settings.Default.RememberKBD = rememberKBD.Checked;

            Properties.Settings.Default.LightOffWhileFS = checkTurnKBLightOff.Checked;

            Properties.Settings.Default.Save();
        }
        #endregion

        bool hide_me = false;

        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                    .IsInRole(WindowsBuiltInRole.Administrator);
        }
        static Form1 frm;
        public Form1()
        {
            InitializeComponent();
            frm = this;
            SystemEvents.PowerModeChanged += OnPowerChange;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (this.WindowState == FormWindowState.Minimized) this.Hide();
        }

        Ols ols;
        const string error_loading_driver_tvicport = "There was an error loading the TVicPort driver. Please reinstall the application or reboot the machine.\r\n\r\nAlternatively, try running the application using the WinRing0 driver. Hold down [SHIFT] while launching the application in order to select another driver to run the application with.";
        const string error_loading_driver_winring = "There was an error loading the WinRing0 driver. Please reinstall the application or reboot the machine.\r\n\r\nAlternatively, you could try installing and using the TVicPort driver. Hold down [SHIFT] while launching the application in order to select another driver to run the application with.";
        const string error_winring_running_standard = "When running using the WinRing0 driver, this application requires administrative privileges at all times. From within the UI interface, you can configure the application to run at startup using administartive privileges without displaying this prompt.\r\n\r\nWould you like to attempt to relaunch this application with administrative privileges now?\r\n\r\nIf you would like to choose which driver to use again, click No in this request, and hold down the [SHIFT] key while launching again the application.";
        const string unable_to_measure_hdd = "The application can't measure disk activity. This is most likely caused by disk performance counters being disabled on this system.\r\n\r\nWould you like this application to attempt to fix this automatically, by enabling the disk performance counters on your system? The application requires administrative privileges to do this.\r\n\r\nAlternatively, run \"LODCTR /E:PerfDisk\", followed by \"diskperf -Y\" from an Administrator Command Prompt.\r\n\r\nIf the issue persists after applying the fix, contact the developer of the application for more information, using e-mail: vali@valinet.ro, or the Reddit thread.";
        const string failed_auto_fix = "Unable to apply the automatic fix, please try the manual procedure:\r\n\r\nRun \"lodctr /e:PerfDisk\", followed by \"diskperf -Y\" from an Administrator Command Prompt.\r\n\r\nIf the issue persists after applying the fix, contact the developer of the application for more information, using e-mail: vali@valinet.ro, or the Reddit thread.";
        const string success_auto_fix = "Disk performance counters are now enabled on this machine.\r\n\r\nThe fix was successful. Press OK to launch the application.";
        const string do_not_show_again = "Do not show this message again";
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string[] cmd = Environment.GetCommandLineArgs();
            string prev = "";
            foreach (string s in cmd)
            {
                switch (s)
                {
                    case "0":
                        if (prev == "driver")
                        {
                            Properties.Settings.Default.Driver = 0;
                            Properties.Settings.Default.Save();
                        }
                        break;
                    case "1":
                        if (prev == "driver")
                        {
                            Properties.Settings.Default.Driver = 1;
                            Properties.Settings.Default.Save();
                        }
                        break;
                }
                prev = s;
            }

            if (Properties.Settings.Default.Driver == 0)
            {
                if (!IsAdministrator())
                {
                    DialogResult dr;
                    if (cmd.Contains("runas")) dr = DialogResult.Yes;
                    else dr = MessageBox.Show(error_winring_running_standard, "ThinkPad LEDs Control", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (dr == DialogResult.Yes)
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.UseShellExecute = true;
                        startInfo.WorkingDirectory = Environment.CurrentDirectory;
                        startInfo.FileName = Application.ExecutablePath;
                        startInfo.Verb = "runas";
                        string args = "";
                        for (int i = 1; i < cmd.Length; i++)
                        {
                            if (i == cmd.Length - 1) args += cmd[i];
                            else args += cmd[i] + " ";
                        }
                        startInfo.Arguments = args;

                        try
                        {
                            Process.Start(startInfo);
                        }
                        catch
                        {
                        }
                        finally
                        {
                            Environment.Exit(0);
                        }

                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                ols = new Ols();
                if (ols.GetStatus() != (uint)Ols.Status.NO_ERROR)
                {
                    MessageBox.Show(error_loading_driver_winring, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }
            else if (Properties.Settings.Default.Driver == 1)
            {
                TVicPort.OpenTVicPort();
                if (TVicPort.IsDriverOpened() == 0)
                {
                    MessageBox.Show(error_loading_driver_tvicport, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }

            foreach (string s in cmd)
            {
                switch (s)
                {
                    case "minimize":
                        this.WindowState = FormWindowState.Minimized;
                        hide_me = true;
                        timer1.Enabled = true;
                        break;
                    case "exit":
                        SaveSettings();
                        Environment.Exit(0);
                        break;
                    case "on":
                        switch (prev)
                        {
                            case "LEDPower":
                                LED(LEDs.Power, PowerStates.On);
                                break;
                            case "LEDRedDot":
                                LED(LEDs.RedDot, PowerStates.On);
                                break;
                            case "LEDMicrophone":
                                LED(LEDs.Microphone, PowerStates.On);
                                break;
                            case "LEDSleep":
                                LED(LEDs.Sleep, PowerStates.On);
                                break;
                            case "LEDFnLock":
                                LED(LEDs.Fn, PowerStates.On);
                                break;
                        }
                        break;
                    case "off":
                        switch (prev)
                        {
                            case "LEDPower":
                                LED(LEDs.Power, PowerStates.Off);
                                break;
                            case "LEDRedDot":
                                LED(LEDs.RedDot, PowerStates.Off);
                                break;
                            case "LEDMicrophone":
                                LED(LEDs.Microphone, PowerStates.Off);
                                break;
                            case "LEDSleep":
                                LED(LEDs.Sleep, PowerStates.Off);
                                break;
                            case "LEDFnLock":
                                LED(LEDs.Fn, PowerStates.Off);
                                break;
                        }
                        break;
                    case "third":
                        switch (prev)
                        {
                            case "LEDPower":
                                LED(LEDs.Power, PowerStates.Blink);
                                break;
                            case "LEDRedDot":
                                LED(LEDs.RedDot, PowerStates.Blink);
                                break;
                            case "LEDMicrophone":
                                LED(LEDs.Microphone, PowerStates.Blink);
                                break;
                            case "LEDSleep":
                                LED(LEDs.Sleep, PowerStates.Blink);
                                break;
                            case "LEDFnLock":
                                LED(LEDs.Fn, PowerStates.Blink);
                                break;
                        }
                        break;
                }
                prev = s;
            }

            ReadSettings();

            PowerManager.IsMonitorOnChanged += new EventHandler(MonitorOnChanged);

            ReadSettingsKBD();


            if (rememberKBD.Checked) lightTimer.Enabled = true;

            if (!PerformanceCounterCategory.Exists("LogicalDisk"))
            {
                if (Properties.Settings.Default.DiskWarning == false)
                {
                    disk_counters_disabled();
                }
                checkHDD.Checked = true;
                checkHDD.Enabled = false;
                /*MessageBox.Show("Object LogicalDisk does not exist!", "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);*/
            }

            else if (!PerformanceCounterCategory.CounterExists("Disk Read Bytes/sec", "LogicalDisk"))
            {
                if (Properties.Settings.Default.DiskWarning == false)
                {
                    disk_counters_disabled();
                }
                checkHDD.Checked = true;
                checkHDD.Enabled = false;
                /*MessageBox.Show("Disk Read Counter not found", "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);*/
            }
            else if (!PerformanceCounterCategory.CounterExists("Disk Write Bytes/sec", "LogicalDisk"))
            {
                if (Properties.Settings.Default.DiskWarning == false)
                {
                    disk_counters_disabled();
                }
                checkHDD.Checked = true;
                checkHDD.Enabled = false;
                /*MessageBox.Show("Disk Write Counter not found", "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);*/
            }
            else
            {
                ReadCounter = new PerformanceCounter("LogicalDisk", "Disk Read Bytes/sec", "_Total");
                WriteCounter = new PerformanceCounter("LogicalDisk", "Disk Write Bytes/sec", "_Total");
                if (Properties.Settings.Default.DiskWarning == true)
                {
                    Properties.Settings.Default.DiskWarning = false;
                    Properties.Settings.Default.Save();
                }
            }
            workerHDD.RunWorkerAsync();

            NotifyIcon1.Text = "ThinkPad LEDs Control";

            if (!checkBox5.Checked)
            {
                _hookID = SetHook(_proc);
                CheckKeys();
            }

        }

        void disk_counters_disabled()
        {
            MessageBoxEx msg = new MessageBoxEx();
            DialogResult dr = msg.Show("DiskWarning", "true", DialogResult.OK, do_not_show_again, unable_to_measure_hdd, "ThinkPad LEDs Control", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (dr == DialogResult.Yes)
            {
                string output = "";
                Process pr = new Process();
                ProcessStartInfo pi = new ProcessStartInfo();
                pi.FileName = "cmd.exe";
                pi.Arguments = "/c lodctr /Q:PerfDisk";
                pi.RedirectStandardOutput = true;
                pi.RedirectStandardError = true;
                pr.OutputDataReceived += (s, d) => output += d.Data;
                pr.StartInfo = pi;
                pi.UseShellExecute = false;
                pr.Start();
                pr.BeginOutputReadLine();
                pr.WaitForExit();
                pr.CancelOutputRead();
                if (output.Contains("Disabled"))
                {
                    output = "";
                    pi.Arguments = "/c lodctr /E:PerfDisk";
                    pr.Start();
                    pr.BeginOutputReadLine();
                    pr.WaitForExit();
                    pr.CancelOutputRead();
                    pi.Arguments = "/c lodctr /Q:PerfDisk";
                    pr.Start();
                    pr.BeginOutputReadLine();
                    pr.WaitForExit();
                    pr.CancelOutputRead();
                    if (output.Contains("Enabled"))
                    {
                        output = "";
                        pi.Arguments = "/c diskperf -Y";
                        pr.Start();
                        pr.BeginOutputReadLine();
                        pr.WaitForExit();
                        pr.CancelOutputRead();
                        if (output.Contains("Raw counters are also enabled for IOCTL_DISK_PERFORMANCE."))
                        {
                            MessageBox.Show(success_auto_fix, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(failed_auto_fix, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(failed_auto_fix, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(failed_auto_fix, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #region Toggle_buttons
        const string error_text_buttons = "There was an error setting the LED. Probably the driver is not installed, or some other bad thing happened.";
        private void powerOn_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.Power, PowerStates.On)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void powerOff_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.Power, PowerStates.Off)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void powerBlink_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.Power, PowerStates.Blink)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dotOn_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.RedDot, PowerStates.On)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dotOff_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.RedDot, PowerStates.Off)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dotBlink_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.RedDot, PowerStates.Blink)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void microphoneOn_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.Microphone, PowerStates.On)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void microphoneOff_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.Microphone, PowerStates.Off)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void microphoneBlink_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.Microphone, PowerStates.Blink)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void sleepOn_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.Sleep, PowerStates.On)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void sleepOff_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.Sleep, PowerStates.Off)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void sleepBlink_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.Sleep, PowerStates.Blink)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region HDDActivity
        static PerformanceCounter ReadCounter;
        static PerformanceCounter WriteCounter;
        bool InProc = true;
        int caps_lock_delay = 100;
        int caps_lock = 0;
        bool isCapsLock()
        {
            return Control.IsKeyLocked(Keys.CapsLock);
        }
        bool isNumLock()
        {
            return Control.IsKeyLocked(Keys.NumLock);
        }
        private void workerHDD_DoWork(object sender, DoWorkEventArgs e)
        {
            Int16 C = default(Int16);
            float R = 0;
            float W = 0;

            while (InProc)
            {
                if (!checkHDD.Checked)
                {
                    R = ReadCounter.NextValue();
                    W = WriteCounter.NextValue();

                    if (R > 0 || W > 0)
                    {
                        if (R > 0)
                        {
                            if (checkHDDReadDot.Checked) LED(LEDs.RedDot, PowerStates.On);
                            if (checkHDDReadMicrophone.Checked) LED(LEDs.Microphone, PowerStates.On);
                            if (checkHDDReadPower.Checked) LED(LEDs.Power, PowerStates.On);
                            if (checkHDDReadSleep.Checked) LED(LEDs.Sleep, PowerStates.On);
                            if (checkHDDReadFn.Checked) LED(LEDs.Fn, PowerStates.On);
                        }
                        if (W > 0)
                        {
                            if (checkHDDWriteDot.Checked) LED(LEDs.RedDot, PowerStates.On);
                            if (checkHDDWriteMicrophone.Checked) LED(LEDs.Microphone, PowerStates.On);
                            if (checkHDDWritePower.Checked) LED(LEDs.Power, PowerStates.On);
                            if (checkHDDWriteSleep.Checked) LED(LEDs.Sleep, PowerStates.On);
                            if (checkHDDWriteFn.Checked) LED(LEDs.Fn, PowerStates.On);
                        }
                    }
                    else
                    {
                        if (R <= 0)
                        {
                            if (checkHDDReadDot.Checked) LED(LEDs.RedDot, PowerStates.Off);
                            if (checkHDDReadMicrophone.Checked) LED(LEDs.Microphone, PowerStates.Off);
                            if (checkHDDReadPower.Checked) LED(LEDs.Power, PowerStates.Off);
                            if (checkHDDReadSleep.Checked) LED(LEDs.Sleep, PowerStates.Off);
                            if (checkHDDReadFn.Checked) LED(LEDs.Fn, PowerStates.Off);
                        }
                        if (W <= 0)
                        {
                            if (checkHDDWriteDot.Checked) LED(LEDs.RedDot, PowerStates.Off);
                            if (checkHDDWriteMicrophone.Checked) LED(LEDs.Microphone, PowerStates.Off);
                            if (checkHDDWritePower.Checked) LED(LEDs.Power, PowerStates.Off);
                            if (checkHDDWriteSleep.Checked) LED(LEDs.Sleep, PowerStates.Off);
                            if (checkHDDWriteFn.Checked) LED(LEDs.Fn, PowerStates.Off);
                        }
                    }

                    NotifyIcon1.Icon = LEDControl.Properties.Resources.IdleIcon;

                    if (R > 0 & W > 0)
                    {
                        NotifyIcon1.Icon = LEDControl.Properties.Resources.RWIcon;
                    }
                    else if (R > 0)
                    {
                        NotifyIcon1.Icon = LEDControl.Properties.Resources.ReadIcon;
                    }
                    else if (W > 0)
                    {
                        NotifyIcon1.Icon = LEDControl.Properties.Resources.WriteIcon;
                    }

                    C = 0;
                    while (C < 10 & InProc)
                    {
                        Application.DoEvents();
                        System.Threading.Thread.Sleep((int)numericUpDown2.Value);
                        C += 1;
                    }
                    if (checkHDD.Checked) caps_lock = caps_lock_delay;
                    else caps_lock++;
                }
                if ((!IsAdministrator() && !checkBox5.Checked && caps_lock == caps_lock_delay) || hide_me)
                {
                    caps_lock = 0;
                    CheckKeys();
                    if (checkHDD.Checked)
                    {
                        caps_lock = caps_lock_delay;
                        System.Threading.Thread.Sleep(caps_lock_delay);
                    }
                }
                if (hide_me)
                {
                    DoOnUIThread(delegate ()
                    {
                        this.Hide();
                    });
                    hide_me = false;
                }
                if (workerHDD.CancellationPending) break;
            }
        }
        public void CheckKeys()
        {
            if (isCapsLock())
            {
                if (checkCLDot.Checked) LED(LEDs.RedDot, PowerStates.On);
                if (checkCLPower.Checked) LED(LEDs.Power, PowerStates.On);
                if (checkCLMicrophone.Checked) LED(LEDs.Microphone, PowerStates.On);
                if (checkCLSleep.Checked) LED(LEDs.Sleep, PowerStates.On);
                if (checkCLFn.Checked) LED(LEDs.Fn, PowerStates.On);
            }
            else
            {
                if (checkCLDot.Checked) LED(LEDs.RedDot, PowerStates.Off);
                if (checkCLPower.Checked) LED(LEDs.Power, PowerStates.Off);
                if (checkCLMicrophone.Checked) LED(LEDs.Microphone, PowerStates.Off);
                if (checkCLSleep.Checked) LED(LEDs.Sleep, PowerStates.Off);
                if (checkCLFn.Checked) LED(LEDs.Fn, PowerStates.Off);
            }
            if (isNumLock())
            {
                if (checkNLDot.Checked) LED(LEDs.RedDot, PowerStates.On);
                if (checkNLPower.Checked) LED(LEDs.Power, PowerStates.On);
                if (checkNLMicrophone.Checked) LED(LEDs.Microphone, PowerStates.On);
                if (checkNLSleep.Checked) LED(LEDs.Sleep, PowerStates.On);
                if (checkNLFn.Checked) LED(LEDs.Fn, PowerStates.On);
            }
            else
            {
                if (checkNLDot.Checked) LED(LEDs.RedDot, PowerStates.Off);
                if (checkNLPower.Checked) LED(LEDs.Power, PowerStates.Off);
                if (checkNLMicrophone.Checked) LED(LEDs.Microphone, PowerStates.Off);
                if (checkNLSleep.Checked) LED(LEDs.Sleep, PowerStates.Off);
                if (checkNLFn.Checked) LED(LEDs.Fn, PowerStates.Off);
            }
        }
        private void DoOnUIThread(MethodInvoker d)
        {
            if (this.InvokeRequired) { this.Invoke(d); } else { d(); }
        }
        #endregion

        #region CheckBoxes
        private void checkHDDReadPower_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHDDReadPower.Checked && checkHDDWritePower.Checked) checkHDDPower.CheckState = CheckState.Checked;
            else
            {
                if (checkHDDReadPower.Checked || checkHDDWritePower.Checked) checkHDDPower.CheckState = CheckState.Indeterminate;
                else checkHDDPower.CheckState = CheckState.Unchecked;
            }
        }

        private void checkHDDReadDot_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHDDReadDot.Checked && checkHDDWriteDot.Checked) checkHDDDot.CheckState = CheckState.Checked;
            else
            {
                if (checkHDDReadDot.Checked || checkHDDWriteDot.Checked) checkHDDDot.CheckState = CheckState.Indeterminate;
                else checkHDDDot.CheckState = CheckState.Unchecked;
            }
        }

        private void checkHDDReadMicrophone_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHDDReadMicrophone.Checked && checkHDDWriteMicrophone.Checked) checkHDDMicrophone.CheckState = CheckState.Checked;
            else
            {
                if (checkHDDReadMicrophone.Checked || checkHDDWriteMicrophone.Checked) checkHDDMicrophone.CheckState = CheckState.Indeterminate;
                else checkHDDMicrophone.CheckState = CheckState.Unchecked;
            }
        }

        private void checkHDDReadSleep_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHDDReadSleep.Checked && checkHDDWriteSleep.Checked) checkHDDSleep.CheckState = CheckState.Checked;
            else
            {
                if (checkHDDReadSleep.Checked || checkHDDWriteSleep.Checked) checkHDDSleep.CheckState = CheckState.Indeterminate;
                else checkHDDSleep.CheckState = CheckState.Unchecked;
            }
        }

        private void checkHDDWritePower_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHDDReadPower.Checked && checkHDDWritePower.Checked) checkHDDPower.CheckState = CheckState.Checked;
            else
            {
                if (checkHDDReadPower.Checked || checkHDDWritePower.Checked) checkHDDPower.CheckState = CheckState.Indeterminate;
                else checkHDDPower.CheckState = CheckState.Unchecked;
            }
        }

        private void checkHDDWriteDot_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHDDReadDot.Checked && checkHDDWriteDot.Checked) checkHDDDot.CheckState = CheckState.Checked;
            else
            {
                if (checkHDDReadDot.Checked || checkHDDWriteDot.Checked) checkHDDDot.CheckState = CheckState.Indeterminate;
                else checkHDDDot.CheckState = CheckState.Unchecked;
            }
        }

        private void checkHDDWriteMicrophone_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHDDReadMicrophone.Checked && checkHDDWriteMicrophone.Checked) checkHDDMicrophone.CheckState = CheckState.Checked;
            else
            {
                if (checkHDDReadMicrophone.Checked || checkHDDWriteMicrophone.Checked) checkHDDMicrophone.CheckState = CheckState.Indeterminate;
                else checkHDDMicrophone.CheckState = CheckState.Unchecked;
            }
        }

        private void checkHDDWriteSleep_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHDDReadSleep.Checked && checkHDDWriteSleep.Checked) checkHDDSleep.CheckState = CheckState.Checked;
            else
            {
                if (checkHDDReadSleep.Checked || checkHDDWriteSleep.Checked) checkHDDSleep.CheckState = CheckState.Indeterminate;
                else checkHDDSleep.CheckState = CheckState.Unchecked;
            }
        }
        #endregion

        #region NotifyIcon
        private void NotifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            InProc = false;
            SaveSettings();
            SaveSettingsKBD();
            NotifyIcon1.Visible = false;
            UnhookWindowsHookEx(_hookID);
            if (Properties.Settings.Default.Driver == 0)
            {
                ols.DeinitializeOls();
            }
            else if (Properties.Settings.Default.Driver == 1)
            {
                TVicPort.CloseTVicPort();
            }
            Environment.Exit(0);
        }
        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            InProc = false;
            SaveSettings();
            SaveSettingsKBD();
            NotifyIcon1.Visible = false;
            if (!checkBox5.Checked) UnhookWindowsHookEx(_hookID);
            if (Properties.Settings.Default.Driver == 0)
            {
                ols.DeinitializeOls();
            }
            else if (Properties.Settings.Default.Driver == 1)
            {
                TVicPort.CloseTVicPort();
            }
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ThinkPad LEDs Control " + Application.ProductVersion + "\r\n\r\nUses code from the great open-source project TPFanControl, available at https://sourceforge.net/projects/tp4xfancontrol/. Contains demo code from Windows Dev Center available at https://code.msdn.microsoft.com/windowsapps/Disk-Activity-Task-bar-af8ae245, and http://blogs.msdn.com/b/toub/archive/2006/05/03/589423.aspx.\r\n\r\nBased on code from the Differentiated System Description Table of the ThinkPad W540/W541 from Lenovo, disassambled using iASL.\r\n\r\nCopyright(c) 2006-2016 ValiNet (Valentin-Gabriel Radu)\r\n\r\nPermission to use, copy, modify, and / or distribute this software for any purpose with or without fee is hereby granted, provided that the above copyright notice and this permission notice appear in all copies.\r\n\r\n" + "Driver: " + (Properties.Settings.Default.Driver == 1 ? "TVicPort" : "WinRing0") + "\r\n" + "Running as Administartor: " + (IsAdministrator() ? "Yes" : "No") + "\r\n\r\nTHE SOFTWARE IS PROVIDED \"AS IS\" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS.IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.\r\nOSS", "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            caps_lock_delay = (int)numericUpDown1.Value;
            if (checkHDD.Checked) caps_lock_delay = caps_lock;
            else caps_lock = 0;
        }

        private void checkHDD_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHDD.Checked)
            {
                checkHDDReadDot.Enabled = false;
                checkHDDReadPower.Enabled = false;
                checkHDDReadMicrophone.Enabled = false;
                checkHDDReadSleep.Enabled = false;
                checkHDDWriteDot.Enabled = false;
                checkHDDWritePower.Enabled = false;
                checkHDDWriteMicrophone.Enabled = false;
                checkHDDWriteSleep.Enabled = false;
                checkHDDReadFn.Enabled = false;
                checkHDDWriteFn.Enabled = false;
                numericUpDown2.Enabled = false;
                caps_lock = caps_lock_delay;
                NotifyIcon1.Icon = LEDControl.Properties.Resources.IdleIcon;
                if (IsAdministrator() || checkBox5.Checked) InProc = false;
            }
            else
            {
                checkHDDReadDot.Enabled = true;
                checkHDDReadPower.Enabled = true;
                checkHDDReadMicrophone.Enabled = true;
                checkHDDReadSleep.Enabled = true;
                checkHDDWriteDot.Enabled = true;
                checkHDDWritePower.Enabled = true;
                checkHDDWriteMicrophone.Enabled = true;
                checkHDDWriteSleep.Enabled = true;
                checkHDDReadFn.Enabled = true;
                checkHDDWriteFn.Enabled = true;
                numericUpDown2.Enabled = true;
                NotifyIcon1.Icon = LEDControl.Properties.Resources.IdleIcon;
                if (IsAdministrator() || checkBox5.Checked)
                {
                    InProc = true;
                    if (!workerHDD.IsBusy) workerHDD.RunWorkerAsync();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This option completely disables the disk monitoring feature, including the tray icon. Only Caps Lock and Num Lock keys monitoring will be available as an option, and by modifying the delay you can reduce the CPU usage this application generates.\r\n\r\nYou may not be able to toggle the disk monitoring feature on/off if disk performance counters are disabled on your system. In order to enable them, open Command Prompt as Administartor, type \"lodctr /E:PerfDisk\", followed by \"diskperf -Y\", and open the application to try again.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Modify the delay between application checks for the Caps Lock key status. A value of 100 (ms) is almost instantaneous, but you can use something like 500 to spare some CPU power, but notice some latency between the physical key press, and the time the LED changes.\r\n\r\nPlease press Enter after modifying the value in order to apply changes.\r\n\r\n1 sec = 1000 ms\r\n\r\nThe delay is necessary because the keyboard hook the application installs in the system when open can monitor only applications running with the same privileges as this application. Thus, the keyboard hook does not get notifyed by the system when the Caps Lock key state changes if that happens from an application run as administrator, and this application was not run as administrator. Ideally, in order to spare the maximum CPU power, only the keyboard hook should be used. But because that gives limited functionality in some scenarios, the application is designed to disable the delay altogether and use the hook exclusively only when run as administrator. So, in order to have the maximum performance, please run this application as administrator. In order to run as administrator at startup without requesting elevation (UAC prompt), run the application as administrator, and click the button 'Register to run at startup as admin' in the application to register a scheduled task that will start the app at logon.\r\n");
        }

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;
                if (key == Keys.Capital)
                {
                    if (!Control.IsKeyLocked(Keys.CapsLock))
                    {
                        if (frm.checkCLDot.Checked) frm.LED(LEDs.RedDot, PowerStates.On);
                        if (frm.checkCLPower.Checked) frm.LED(LEDs.Power, PowerStates.On);
                        if (frm.checkCLMicrophone.Checked) frm.LED(LEDs.Microphone, PowerStates.On);
                        if (frm.checkCLSleep.Checked) frm.LED(LEDs.Sleep, PowerStates.On);
                        if (frm.checkCLFn.Checked) frm.LED(LEDs.Fn, PowerStates.On);
                    }
                    else
                    {
                        if (frm.checkCLDot.Checked) frm.LED(LEDs.RedDot, PowerStates.Off);
                        if (frm.checkCLPower.Checked) frm.LED(LEDs.Power, PowerStates.Off);
                        if (frm.checkCLMicrophone.Checked) frm.LED(LEDs.Microphone, PowerStates.Off);
                        if (frm.checkCLSleep.Checked) frm.LED(LEDs.Sleep, PowerStates.Off);
                        if (frm.checkCLFn.Checked) frm.LED(LEDs.Fn, PowerStates.Off);
                    }
                }
                if (key == Keys.NumLock)
                {
                    if (!Control.IsKeyLocked(Keys.NumLock))
                    {
                        if (frm.checkNLDot.Checked) frm.LED(LEDs.RedDot, PowerStates.On);
                        if (frm.checkNLPower.Checked) frm.LED(LEDs.Power, PowerStates.On);
                        if (frm.checkNLMicrophone.Checked) frm.LED(LEDs.Microphone, PowerStates.On);
                        if (frm.checkNLSleep.Checked) frm.LED(LEDs.Sleep, PowerStates.On);
                        if (frm.checkNLFn.Checked) frm.LED(LEDs.Fn, PowerStates.On);
                    }
                    else
                    {
                        if (frm.checkNLDot.Checked) frm.LED(LEDs.RedDot, PowerStates.Off);
                        if (frm.checkNLPower.Checked) frm.LED(LEDs.Power, PowerStates.Off);
                        if (frm.checkNLMicrophone.Checked) frm.LED(LEDs.Microphone, PowerStates.Off);
                        if (frm.checkNLSleep.Checked) frm.LED(LEDs.Sleep, PowerStates.Off);
                        if (frm.checkNLFn.Checked) frm.LED(LEDs.Fn, PowerStates.Off);
                    }
                }
                    //Console.WriteLine((Keys)vkCode);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                numericUpDown1.UpButton();
                numericUpDown1.DownButton();
                caps_lock_delay = (int)numericUpDown1.Value;
                if (checkHDD.Checked) caps_lock = caps_lock_delay;
                else caps_lock = 0;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try {
                // Creates an XML file from which a scheduled task is created and saved on the system. Based on an example found on StackOverflow, http://stackoverflow.com/questions/5427673/how-to-run-a-program-automatically-as-admin-on-windows-startup
                string username = Environment.UserName;
                string computername = Environment.MachineName;
                string part1 = LEDControl.Properties.Resources.part1;
                string part2 = LEDControl.Properties.Resources.part2;
                string part3 = LEDControl.Properties.Resources.part3;
                string part4 = LEDControl.Properties.Resources.part4;
                string final = part1 + username + part2 + computername + "\\" + username + part3 + Application.ExecutablePath + part4;
                System.IO.StreamWriter sw = new System.IO.StreamWriter(Application.StartupPath + "\\apply.xml");
                sw.Write(final);
                sw.Close();
                Process pr = new Process();
                ProcessStartInfo pi = new ProcessStartInfo();
                pi.FileName = "cmd.exe";
                pi.Arguments = "/c schtasks /create /tn \"Start ThinkPad LEDs Control elevated\" /xml \"" + Application.StartupPath + "\\apply.xml\"";
                pr.StartInfo = pi;
                pr.Start();
                pr.WaitForExit();
                DialogResult dr = MessageBox.Show("Registration complete, do you want to open Task Scheduler to check the operation, or further adjust the entry which was created?", "ThinkPad LEDs Control", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes) Process.Start("Taskschd.msc");
                System.IO.File.Delete(Application.StartupPath + "\\apply.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to finish this task.\r\n\r\n" + ex.Message, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Set the delay between checks for disk access are performed. The smaller the value, the more accurate the LED will light, but the higher the CPU overhead. Depending on your taste, you may wish to keep a balance (like, perform 10 checks per second), or use the default setting, which is maximum accuracy. Please take note that there may also be a delay between the command to set the LED is issued, and the actual LED turning on/off, due to physical travel, and the multiple layers through which this change occurs (the change is first written to the embedded controller of the system, which then sends it to the digital logic which turns the LED on/off).");
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkCLDot.Enabled = false;
                checkCLPower.Enabled = false;
                checkCLMicrophone.Enabled = false;
                checkCLSleep.Enabled = false;
                checkCLFn.Enabled = false;
                checkNLDot.Enabled = false;
                checkNLPower.Enabled = false;
                checkNLMicrophone.Enabled = false;
                checkNLSleep.Enabled = false;
                checkNLFn.Enabled = false;
                numericUpDown1.Enabled = false;
                if (checkHDD.Checked) InProc = false;
                UnhookWindowsHookEx(_hookID);
                //NotifyIcon1.Visible = false;
            }
            else
            {
                checkCLDot.Enabled = true;
                checkCLPower.Enabled = true;
                checkCLMicrophone.Enabled = true;
                checkCLSleep.Enabled = true;
                checkCLFn.Enabled = true;
                checkNLDot.Enabled = true;
                checkNLPower.Enabled = true;
                checkNLMicrophone.Enabled = true;
                checkNLSleep.Enabled = true;
                checkNLFn.Enabled = true;
                numericUpDown1.Enabled = true;
                _hookID = SetHook(_proc);
                if (checkHDD.Checked)
                {
                    InProc = true;
                    if (!workerHDD.IsBusy) workerHDD.RunWorkerAsync();
                }
                CheckKeys();
                //NotifyIcon1.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Disables the key monitoring functionality of the application.\r\nThis includes also uninstalling or installing the keyboard hook that provides real time status of the keys (without delay).");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hide_me)
            {
                DoOnUIThread(delegate ()
                {
                    this.Hide();
                });
                hide_me = false;
                timer1.Enabled = false;
            }
        }

        bool wasRunning = false;
        void OnPowerChange(Object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    if (wasRunning)
                    {
                        wasRunning = false;
                        workerHDD.RunWorkerAsync();
                    }
                    CheckKeys();
                    CheckKeys();
                    CheckKeys();
                    CheckKeys();
                    var query = (from item in levels
                                 group item by item into g
                                 orderby g.Count() descending
                                 select g.Key).First();
                    SetKeyboardLevel(query);
                    if (rememberKBD.Checked) lightTimer.Enabled = true;
                    break;
                case PowerModes.Suspend:
                    //SaveSettingsKBD();
                    if (workerHDD.IsBusy)
                    {
                        wasRunning = true;
                        workerHDD.CancelAsync();
                    }
                    break;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Custom c = new Custom();
            DialogResult dr;
            do
            {
                dr = c.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    byte t = Byte.Parse(c.textBox1.Text, System.Globalization.NumberStyles.HexNumber);
                    byte _out = (byte)(Byte.Parse(c.textBox1.Text, System.Globalization.NumberStyles.HexNumber) | Byte.Parse(c.textBox2.Text, System.Globalization.NumberStyles.HexNumber));
                    WriteByteToEC(Byte.Parse(c.textBox3.Text, System.Globalization.NumberStyles.HexNumber), _out); //0x0d for keybaord illumination
                    System.Media.SystemSounds.Asterisk.Play();
                }
            }
            while (dr == DialogResult.OK);

        }

        private enum LightLevel
        {
            Off, Low, High, Unknown
        }

        private LightLevel GetKeyboardLightlevel()
        {
            byte c = 0x00;
            ReadByteFromEC(0x0d, ref c);
            if (c >= 0 && c < 50) return LightLevel.Off;
            else if (c >= 50 && c < 100) return LightLevel.Low;
            else if (c >= 100 && c < 150) return LightLevel.High;
            else return LightLevel.Unknown;
        }
        
        private void SetKeyboardLevel(LightLevel lvl)
        {
            byte _out = 0x00;
            switch (lvl)
            {
                case LightLevel.Off:
                    _out = 0x00 | 0x00;
                    WriteByteToEC(0x0d, _out);
                    break;
                case LightLevel.Low:
                    _out = 0x00 | 0x40;
                    WriteByteToEC(0x0d, _out);
                    break;
                case LightLevel.High:
                    _out = 0x00 | 0x80;
                    WriteByteToEC(0x0d, _out);
                    break;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
             byte c = 0x00;
             ReadByteFromEC(0x0d, ref c);
             MessageBox.Show(c.ToString());
        }
        //end-
        bool prevStat = true;
        void MonitorOnChanged(object sender, EventArgs e)
        {
            if (rememberKBD.Checked)
            {
                if (prevStat != PowerManager.IsMonitorOn)
                {
                    prevStat = PowerManager.IsMonitorOn;
                }
                if (PowerManager.IsMonitorOn == true)
                {
                    var query = (from item in levels
                                 group item by item into g
                                 orderby g.Count() descending
                                 select g.Key).First();
                    SetKeyboardLevel(query);
                    lightTimer.Enabled = true;
                }
                else lightTimer.Enabled = false;
                //SaveSettingsKBD();
            }
        }
        List<LightLevel> levels = new List<LightLevel>();
        bool prev_l = false;
        bool prev_v = false;
        LightLevel prev_c;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        public static string GetText(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        private List<MMDevice> gMicrophoneDevices = new List<MMDevice>();//global variable
        private bool findMicrophones()
        {
            MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
            MMDeviceCollection devices = DevEnum.EnumerateAudioEndPoints(EDataFlow.eCapture, EDeviceState.DEVICE_STATE_ACTIVE);
            for (int i = 0; i < devices.Count; i++)
            {
                MMDevice deviceAt = devices[i];
                if (deviceAt.FriendlyName.ToLower() == "microphone array")
                    gMicrophoneDevices.Add(deviceAt);
            }
            if (gMicrophoneDevices.Count == 0)
                return false;
            else return true;
        }
        private bool isVolumeMute()
        {
            MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
            MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
            return defaultDevice.AudioEndpointVolume.Mute;
        }

        private void lightTimer_Tick(object sender, EventArgs e)
        {
            bool full = false;
            if (checkTurnKBLightOff.Checked)
            {
                string title = GetText(GetForegroundWindow());
                Console.WriteLine(title);
                foreach (Screen s in Screen.AllScreens)
                    if (IsForegroundFullScreen() && title != "" && title != "Windows Default Lock Screen")
                    {
                        if (!prev_l) prev_c = GetKeyboardLightlevel();
                        if (!prev_l) prev_v = true;
                        prev_l = true;
                        full = true;
                    }
            }
            if (PowerManager.IsMonitorOn && (!checkTurnKBLightOff.Checked || !full))
            {
                levels.Add(GetKeyboardLightlevel());
                if (levels.Count > 5) levels.RemoveAt(0);
            }
            if (checkTurnKBLightOff.Checked)
            {
                if (full)
                {
                    if (prev_v)
                    {
                        SetKeyboardLevel(LightLevel.Off);
                        prev_v = false;
                        LED(LEDs.Microphone, PowerStates.Off, false);
                        LED(LEDs.Fn, PowerStates.Off, false);
                        LED(LEDs.Power, PowerStates.Off, false);
                    }
                }
                else
                {
                    if (prev_l)
                    {
                        prev_l = false;
                        LightLevel lvl = GetKeyboardLightlevel();
                        if (lvl == LightLevel.Off) SetKeyboardLevel(prev_c);
                        gMicrophoneDevices.Clear();
                        findMicrophones();
                        if (gMicrophoneDevices[0].AudioEndpointVolume.Mute == true) LED(LEDs.Microphone, PowerStates.On, false);
                        LED(LEDs.Power, PowerStates.On, false);
                        CheckKeys();
                    }
                }
            }
        }

        private void rememberKBD_CheckedChanged(object sender, EventArgs e)
        {
            lightTimer.Enabled = rememberKBD.Checked;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(HandleRef hWnd, [In, Out] ref RECT rect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public static bool IsForegroundFullScreen()
        {
            return IsForegroundFullScreen(null);
        }

        public static bool IsForegroundFullScreen(Screen screen)
        {
            if (screen == null)
            {
                screen = Screen.PrimaryScreen;
            }
            RECT rect = new RECT();
            GetWindowRect(new HandleRef(null, GetForegroundWindow()), ref rect);
            return new Rectangle(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top).Contains(screen.Bounds);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.Fn, PowerStates.On)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.Fn, PowerStates.Off)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!LED(LEDs.Fn, PowerStates.Blink)) MessageBox.Show(error_text_buttons, "ThinkPad LEDs Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void checkHDDReadFn_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHDDReadFn.Checked && checkHDDWriteFn.Checked) checkHDDFn.CheckState = CheckState.Checked;
            else
            {
                if (checkHDDReadFn.Checked || checkHDDWriteFn.Checked) checkHDDFn.CheckState = CheckState.Indeterminate;
                else checkHDDFn.CheckState = CheckState.Unchecked;
            }

        }

        private void checkHDDWriteFn_CheckedChanged(object sender, EventArgs e)
        {
            if (checkHDDReadFn.Checked && checkHDDWriteFn.Checked) checkHDDFn.CheckState = CheckState.Checked;
            else
            {
                if (checkHDDReadFn.Checked || checkHDDWriteFn.Checked) checkHDDFn.CheckState = CheckState.Indeterminate;
                else checkHDDFn.CheckState = CheckState.Unchecked;
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Welcome w = new Welcome();
            w.comboBox1.SelectedIndex = Properties.Settings.Default.Driver;
            if (w.ShowDialog() == DialogResult.OK)
            {
                SaveSettings();
                Process.Start(Application.ExecutablePath);
                Environment.Exit(0);
            }
        }
    }
}
