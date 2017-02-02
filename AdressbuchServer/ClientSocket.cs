using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace __ClientSocket__ {
    class ClientSocket {
        private Socket socket;
        private int port;
        private IPAddress host;

        public ClientSocket(string host, int port) {
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            this.port = port;
            this.host = Dns.GetHostEntry(host).AddressList[0];
        }

        public ClientSocket(Socket socket) {
            this.socket = socket;
        }

        public bool Connect() {
            try {
                socket.Connect(host, port);
            } catch (Exception e) {
                Debug.WriteLine(e.Message);
                throw;
            }
            return true;
        }

        public int DataAvailable() => socket.Available;

        public void Write(int b) {
            byte[] msg = new byte[1];
            msg[0] = (byte)b;
            socket.Send(msg);
        }

        public void Write(string s) {
            byte[] msg = Encoding.Unicode.GetBytes(s);
            socket.Send(msg);
        }

        public int Read() {
            byte[] rcvbuffer = new byte[1];
            socket.Receive(rcvbuffer);
            return rcvbuffer[0];
        }

        public int Read(byte[] b, int len) => socket.Receive(b, len, SocketFlags.None);

        public string ReadLine() {
            byte[] rcvbuffer = new byte[256];
            socket.Receive(rcvbuffer);
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

            string rcv = "";

            foreach (byte b in rcvbuffer) {
                if (b != '\0')
                    rcv += (char)b;
            }

            if (rcv.Substring(rcv.Length - 1) == "\n")
                rcv = rcv.Remove(rcv.Length - 1, 1);

            return rcv;
        }
        public void Close() => socket.Close();
        public void Dispose() => socket.Dispose();
    }
}