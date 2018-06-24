using System;
using System.Threading;
using System.Windows;
using System.Net.NetworkInformation;
using System.Linq;

namespace CefServer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Default URL Port for App
        internal static int Port = 60256;

        //URL
        internal static string Url = "http://localhost:" + Port;

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

        //Check if url port is free to use, or find next free one
        private static bool CheckPort()
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnectInfoArray = ipGlobalProperties.GetActiveTcpConnections();


            while (tcpConnectInfoArray.Any(tcpi => tcpi.LocalEndPoint.Port == Port))
            {
                Port++;
                if (Port >= 64000)
                    return false;
            }
            return true;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _mutex.ReleaseMutex();

            base.OnExit(e);

            Environment.Exit(Environment.ExitCode);
        }
    }
}
