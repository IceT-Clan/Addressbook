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
        private Encoding encoding = Encoding.UTF8;

        public ClientSocket(string host, int port) {
            //this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            this.port = port;
            if(!IPAddress.TryParse(host, out this.host)) {
                this.host = Dns.GetHostEntry(host).AddressList[0];
            }
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

        public Int32 DataAvailable() => this.socket.Available;

        public void Write(Int32 b) {
            Byte[] msg = new Byte[1];
            msg[0] = (Byte)b;
            this.socket.Send(msg);
        }

        public void Write(string str) => this.socket.Send(this.encoding.GetBytes(str));

        public Char Read() {
            Byte[] rcvbuffer = new Byte[1];
            this.socket.Receive(rcvbuffer);
            return (Char)rcvbuffer[0];
        }

        public Char Read(Byte[] b, Int32 len) => (Char)this.socket.Receive(b, len, SocketFlags.None);

        /// <summary>
        /// Receive bytes from server until newline
        /// </summary>
        /// <returns>String with bytes from server (without newline)</returns>
        public String ReadLine() {
            String rcv = "";
            Byte[] rcvbuffer = new Byte[1];
            rcvbuffer[0] = 0;

            while (rcvbuffer[0] != '\n') {
                this.socket.Receive(rcvbuffer);
                ASCIIEncoding enc = new ASCIIEncoding();
                rcv += this.encoding.GetString(rcvbuffer);
            }
            //return recv.Trim('\0');

            return rcv.Substring(0, rcv.Length - 1);
        }
        public void Close() => this.socket.Close();
        public void Dispose() => this.socket.Dispose();
    }
}