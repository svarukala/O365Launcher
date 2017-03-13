using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O365Launcher.TaskTrayApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            string appProcessName =
                    Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            Process[] RunningProcesses = Process.GetProcessesByName(appProcessName);
            if (RunningProcesses.Length <= 1) // just me, so run!
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LauncherAppContext());
            }
            else // switch to the first instance and exit
            {
                MessageBox.Show("There is an intsance of this app running already. Please check taskbar.");
                //ShowWindowAsync(RunningProcesses[0].MainWindowHandle,
                //    (int)ShowWindowConstants.SW_SHOWMINIMIZED);
                //ShowWindowAsync(RunningProcesses[0].MainWindowHandle,
                //    (int)ShowWindowConstants.SW_RESTORE);
            }
        }
    }
}
