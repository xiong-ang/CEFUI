using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CefServer
{
    public class WebHelper
    {
        private static bool _isStartUp = true;

        private static int _port = 55555;
        public static int AppPort
        {
            get
            {
                if (_isStartUp)
                {
                    _port = GetFreePort(_port);
                    _isStartUp = false;
                }
                return _port; 
            }
        }

        private static int GetFreePort(int startPort)
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //Get TCP Connection Info
            var tcpConnectInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            //Get TCP Listening Info
            var tcpListenInfoArray = ipGlobalProperties.GetActiveTcpListeners();


            while (tcpListenInfoArray.Any(tcpl => tcpl.Port == startPort)
                || tcpConnectInfoArray.Any(tcpi => tcpi.LocalEndPoint.Port == startPort))
            {
                startPort++;
                if (startPort >= 60000)
                    break;
            }

            return startPort;
        }
    }
}
