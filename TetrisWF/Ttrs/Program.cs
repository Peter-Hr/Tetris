using System;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace Ttrs
{
    internal static class Program
    {
        [STAThread]

        static void Main()
        {

            var repo = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(repo, new FileInfo("log4net.config"));

            var log = LogManager.GetLogger(typeof(Program));

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                    log.Fatal("Unhandled exception", ex);
                else
                    log.Fatal("Unhandled non-exception fatal error");
            };

            log.Info("Application starting");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
