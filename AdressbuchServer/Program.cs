using System;

namespace Addressbook {
    class Program {
        static void Main(string[] args) {
            int port = 55555;
            string adressbook = "Addressbook.csv";

            if (args.Length > 0) {
                if (args[0] == "-h" || args[0] == "--help") {
                    Console.WriteLine("Usage: <port> <adressbook>");
                    Environment.Exit(22);
                }

                if (args.Length == 2) {
                    port = Convert.ToInt32(args[0]);
                    adressbook = args[1];
                }
            }

            ControllerServer c = new ControllerServer(port, adressbook);
            c.Start();

        }
    }
}
