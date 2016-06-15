using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEDControl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            const string resource1 = "LEDControl.CoreAudioApi.dll";
            const string resource2 = "LEDControl.Microsoft.WindowsAPICodePack.dll";
            const string resource3 = "LEDControl.CbtHook.dll";
            const string resource4 = "LEDControl.WindowsHook.dll";
            EmbeddedAssembly.Load(resource1, "CoreAudioApi.dll");
            EmbeddedAssembly.Load(resource2, "Microsoft.WindowsAPICodePack.dll");
            EmbeddedAssembly.Load(resource3, "CbtHook.dll");
            EmbeddedAssembly.Load(resource4, "WindowsHook.dll");
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Properties.Settings.Default.UpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }

            if ((Properties.Settings.Default.FirstRun || Control.ModifierKeys == Keys.Shift) && !Environment.GetCommandLineArgs().Contains("driver"))
            {
                Welcome w = new Welcome();
                if (w.ShowDialog() == DialogResult.Cancel)
                {
                    Environment.Exit(0);
                }
            }

            Application.Run(new Form1());
        }
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
    }
}
