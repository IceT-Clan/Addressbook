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

        public ClientSocket(string host, int port) {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.port = port;
            this.host = Dns.GetHostEntry(host).AddressList[0];
        }

        public ClientSocket(Socket socket) {
            this.socket = socket;
        }

        public bool Connect() {
            try {
                this.socket.Connect(this.host, this.port);
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

        public void Write(string s) {
            byte[] msg = Encoding.Unicode.GetBytes(s);
            this.socket.Send(msg);
        }

        public int Read() {
            byte[] rcvbuffer = new byte[1];
            this.socket.Receive(rcvbuffer);
            return rcvbuffer[0];
        }

        public int Read(byte[] b, int len) => socket.Receive(b, len, SocketFlags.None);

        public string ReadLine() {
            byte[] rcvbuffer = new byte[256];
            this.socket.Receive(rcvbuffer);
            ASCIIEncoding encoding = new ASCIIEncoding();
            string recv = "";

            foreach (byte b in rcvbuffer) {
                if (b != '\0')
                    recv += (char)b;
            }

            if (recv.Substring(recv.Length - 1) == "\n")
                recv = recv.Remove(recv.Length - 1, 1);

            return recv;
        }
        public void Close() => this.socket.Close();
        public void Dispose() => this.socket.Dispose();
    }
}