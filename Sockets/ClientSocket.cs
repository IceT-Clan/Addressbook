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
                return false;
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

        /// <summary>
        /// Receive bytes from server until newline
        /// </summary>
        /// <returns>String with bytes from server (without newline)</returns>
        public string ReadLine() {
            string rcv = "";
            byte[] rcvbuffer = new byte[1];
            rcvbuffer[0] = 0;

            while (rcvbuffer[0] != '\n') {
                socket.Receive(rcvbuffer);
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                rcv += Encoding.Unicode.GetString(rcvbuffer);
            }
            //return recv.Trim('\0');

            return rcv.Substring(0, rcv.Length - 1);
        }
        public void Close() => this.socket.Close();
        public void Dispose() => this.socket.Dispose();
    }
}