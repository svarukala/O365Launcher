using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O365Launcher.TaskTrayApp
{
    static class Program
    {
        private static TelemetryClient tc = new TelemetryClient();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            tc.InstrumentationKey = "dc41fcc7-bc20-4b9a-bb0a-bf4815df949c";

            // Set session data:
            tc.Context.User.Id = Environment.MachineName;
            tc.Context.Session.Id = Guid.NewGuid().ToString();
            tc.Context.Device.OperatingSystem = Environment.OSVersion.ToString();


            string appProcessName =
                    Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            Process[] RunningProcesses = Process.GetProcessesByName(appProcessName);
            if (RunningProcesses.Length <= 1) // just me, so run!
            {
                // Add the event handler for handling UI thread exceptions to the event.
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

                // Set the unhandled exception mode to force all Windows Forms errors to go through
                // our handler.
                //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

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

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs t)
        {
            DialogResult result = DialogResult.Cancel;
            try
            {
                tc.TrackException(t.Exception);
                result = ShowThreadExceptionDialog("Windows Forms Error", t.Exception);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Windows Forms Error",
                        "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
                Application.Exit();
        }

        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "An application error occurred. Please contact the adminstrator " +
                "with the following information:\n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }
    }
}
