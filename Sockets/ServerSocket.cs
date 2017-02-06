using System;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace Sockets {
    class ServerSocket {
        private int port;
        private Socket serverSocket;

        public ServerSocket(int port) {
            this.port = port;
            //this.serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = ipHostInfo.AddressList[1];
            //IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
            IPEndPoint localEndPoint = new IPEndPoint(0, port);

            try {
                this.serverSocket.Bind(localEndPoint);
                this.serverSocket.Listen(10);
            } catch (Exception e) {
                Debug.WriteLine(e.Message);
                this.serverSocket.Close();
                return;
            }
        }

        public Socket Accept() => this.serverSocket.Accept();
        public void Close() => this.serverSocket.Close();

    }
}
