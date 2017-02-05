using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace Sockets {
    class ClientSocket {
        private Socket socket;
        private int port;
        private IPAddress host;
        private IPEndPoint endPoint;

        public ClientSocket(string host, int port) {
            //this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            this.port = port;
            this.host = Dns.GetHostEntry(host).AddressList[0];
            this.endPoint = new IPEndPoint (this.host,this.port);
        }

        public ClientSocket(Socket socket) {
            this.socket = socket;
        }

        public bool Connect() {
            try {
                //this.socket.Connect(this.host, this.port);
                this.socket.Connect(this.endPoint);
            } catch (Exception e) {
                Debug.WriteLine(e.Message);
                throw;
            }
            return true;
        }

        public int DataAvailable() => this.socket.Available;

        public void Write(int b) {
            byte[] msg = new byte[1];
            msg[0] = (byte)b;
            this.socket.Send(msg);
        }

        public void Write(string str) => this.socket.Send(Encoding.Unicode.GetBytes(str));

        public char Read() {
            byte[] rcvbuffer = new byte[1];
            this.socket.Receive(rcvbuffer);
            return (char)rcvbuffer[0];
        }

        public char Read(byte[] b, int len) => (char)this.socket.Receive(b, len, SocketFlags.None);

        public string ReadLine() {
            byte[] recvbuffer = new byte[1];
            ASCIIEncoding encoding = new ASCIIEncoding();
            string recv = "";
            bool isNotEnd = true;
    
            while (isNotEnd) {
                this.socket.Receive(recvbuffer);
                if (recvbuffer[0] != '\n') {
                    recv += (char)recvbuffer[0];
                } else
                    isNotEnd = false;
            }
            return recv;
        }
        public void Close() => this.socket.Close();
        public void Dispose() => this.socket.Dispose();
    }
}