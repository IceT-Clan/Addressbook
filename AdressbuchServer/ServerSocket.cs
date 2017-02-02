using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using __ClientSocket__;

namespace __ServerSocket__ {
    class ServerSocket {
        private int port;
        private Socket serverSocket;

        public ServerSocket(int port) {
            this.port = port;
            serverSocket = new Socket(AddressFamily.InterNetwork,
                                         SocketType.Stream,
                                         ProtocolType.Tcp);

            IPAddress hostIP = (Dns.GetHostEntry(IPAddress.Any.ToString())).AddressList[0];
            IPEndPoint ep = new IPEndPoint(hostIP, port);

            // MessageBox.Show(ep.ToString());

            try {
                serverSocket.Bind(ep);
                serverSocket.Listen(1);
            } catch (Exception) {
                // MessageBox.Show(ex.Message);
                serverSocket.Close();
                return;
            }
        }

        public Socket accept() => serverSocket.Accept();

        public void close() => serverSocket.Close();

    }
}
