using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using __ClientSocket__;

namespace __ServerSocket__ {
    class ServerSocket {
        private int port;
        private Socket serverSocket;

        public ServerSocket(int port) {
            this.port = port;
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            try {
                serverSocket.Bind(localEndPoint);
                serverSocket.Listen(10);
            } catch (Exception e) {
                Debug.WriteLine(e.Message);
                serverSocket.Close();
                return;
            }
        }

        //public Socket Accept() => serverSocket.Accept();
        public Socket Accept() {
            return serverSocket.Accept();
        }

        public void Close() => serverSocket.Close();

    }
}
