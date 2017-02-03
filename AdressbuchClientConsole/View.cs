using People;
using System;
using System.Collections.Generic;

namespace Addressbook {
    class View {
        public void ShowMenu() {
            // Ausgabe Menue
            Console.WriteLine(@"          _____ _        _ _    ____              _    ");
            Console.WriteLine(@"         / ____| |      | | |  |  _ \            | |   ");
            Console.WriteLine(@"        | (___ | |_ __ _| | | _| |_) | ___   ___ | | __");
            Console.WriteLine(@"         \___ \| __/ _` | | |/ /  _ < / _ \ / _ \| |/ /");
            Console.WriteLine(@"         ____) | || (_| | |   <| |_) | (_) | (_) |   < ");
            Console.WriteLine(@"        |_____/ \__\__,_|_|_|\_\____/ \___/ \___/|_|\_\");
            Console.WriteLine("");
            Console.WriteLine("             =====================================");
            Console.WriteLine("             =      [1] - Search Person          =");
            Console.WriteLine("             =      [2] - Load Complete A.B      =");
            Console.WriteLine("             =      [3] - Modify Person          =");
            Console.WriteLine("             =      [4] - Delete Person          =");
            Console.WriteLine("             =      [5] - Delete Person IDs      =");
            Console.WriteLine("             =      [6] - Delete AdressBook      =");
            Console.WriteLine("             =      [9] - EXIT                   =");
            Console.WriteLine("             =====================================");
            Console.Write("");
            Console.Write("$ INPUT> ");
        }
        public void Refresh(List<Person> _personen) {
            foreach (Person p in _personen) {
                Console.WriteLine("======================================");
                Console.WriteLine("Name:" + p.Name + " | Birthdata: " + p.Birth_data);
                Console.WriteLine("Adresse: " + p.Address);
                Console.WriteLine("Phone Home: " + p.Phone_h + " | Phone Work: " + p.Phone_w);
                Console.WriteLine("Email: " + p.Email);
                Console.WriteLine("Skin: " + p.Color + "#noracist");
                Console.WriteLine("Height: " + p.Height + " | Weight: " + p.Weight);
                Console.WriteLine("Blood: " + p.Blood + " | Eye: " + p.Eye);
                Console.WriteLine("Hair: " + p.Hair + " | Haircolor: " + p.Hair_color);
                Console.WriteLine("======================================");
                Console.WriteLine();
            }
        }
    }
}
