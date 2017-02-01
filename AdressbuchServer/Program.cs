using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch {
    class Program {
        static void Main(string[] args) {
            int port = 55555;
            string adressbook = "adressbook.csv";

            if (args.Length < 2) {
                Console.WriteLine("Usage: <port> <adressbook>");
            }

            port = Convert.ToInt16(args[0]);
            adressbook = args[1];

            ControllerServer c = new ControllerServer(port, adressbook);
            c.start();

        }
    }
}
