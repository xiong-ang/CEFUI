using System;
using System.Threading;
using System.Windows;

namespace CefServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //URL
        internal static string Url = "http://localhost:" + WebHelper.AppPort;

        //Mutex for App
        private static Mutex _mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            SplashScreen splash = new SplashScreen("splash.jpg");

            //Ensure only one running app
            bool result = false;
            _mutex = new Mutex(true, "CefUI", out result);
            if (!result)
            {
                MessageBox.Show("Application is already running.", "CefUI Error");
                Shutdown();
                Environment.Exit(Environment.ExitCode);
            }

            //Check dependencies

            //Check if there is free url port
            if (!CheckPort())
            {
                MessageBox.Show("Unable to find free url port to run.", "CefUI Error");
                Shutdown();
                Environment.Exit(Environment.ExitCode);
            }

            splash.Show(true, true);

            base.OnStartup(e);
        }

        //Check Port Range
        private static bool CheckPort()
        {
            return WebHelper.AppPort < 60000;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _mutex.ReleaseMutex();

            base.OnExit(e);

            Environment.Exit(Environment.ExitCode);
        }
    }
}
