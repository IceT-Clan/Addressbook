using System;

namespace Addressbook {
    // Das ist der Client
    class Program {
        static void Main(string[] args) {
            // Standardhost ist localhost
            string host = "127.0.0.1";

            // Standardport ist 55555
            int port = 55555;
            if (args.Length > 0) {
                if (args[0] == "-h" || args[0] == "--help") {
                    Console.WriteLine("Usage: host:port");
                    Environment.Exit(22);
                }

                if (args.Length == 1) {
                    host = args[0].Split(':')[0];
                    port = Convert.ToInt32(args[0].Split(':')[1]);
                }
            }

            ControllerClient controller = new ControllerClient(host, port);
            controller.Start();
            Console.WriteLine("Press a Button to exit...");
            Console.ReadKey(false);
        }
    }
}
