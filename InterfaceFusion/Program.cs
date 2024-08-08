using System.Diagnostics;

namespace InterfaceFusion
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
        
            if (PriorProcess() != null)
            {
                MessageBox.Show("La interface ya se encuentra en ejecución");
                return;
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);
            Application.Run(new frmFusion());
            Environment.Exit(1);
        }

        static void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            //Log.Error(e.Exception, e.Exception.Message);
        }

        public static Process PriorProcess()
        { 
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(curr.ProcessName);

            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) && (p.MainModule.FileName == curr.MainModule.FileName))
                { 
                    return p;
                }
            }
            return null;
        }
    }
}