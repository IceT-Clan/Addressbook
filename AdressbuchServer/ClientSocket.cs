using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace __ClientSocket__ {
    class ClientSocket {
        private Socket socket;
        private IPEndPoint ep;
        private IPHostEntry hostInfo;

        public ClientSocket(string host, int port) {
            Dns.GetHostEntry(host);
            ep = new IPEndPoint(hostInfo.AddressList[0], port);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public ClientSocket(Socket socket) {
            this.socket = socket;
        }

        public bool Connect() {
            try {
                socket.Connect(ep);
            } catch (Exception) {
                // MessageBox.Show(ex.Message);
                return false;
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