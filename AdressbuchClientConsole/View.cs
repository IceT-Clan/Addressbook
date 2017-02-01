using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch
{
    class View
    {
        // Hierin wird alles notwenige für
        // das Anzeigen der Daten gekapselt
        // die View soll nach außen festgelegte
        // Methoden enthalten und austauschbar sein
        // z.B. durch ein Formular


        /*






             */


        public void zeigeMenue()
        {
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
        public void aktualisiereSicht(List<Person> _personen)
        {
            //female,Juliane,Fenstermacher,"Schaarsteinweg 16",93491,Stamsried,"Freistaat Bayern",Germany,JulianeFenstermacher@gustr.com,49,"09465 80 57 67",8/8/1991
           
            foreach (Person p in _personen)
            {
                Console.WriteLine("======================================");
                Console.WriteLine("Name:" + p.Name + " | Birthdata: " + p.Birth_data);
                Console.WriteLine("Adresse: "+ p.Address);
                Console.WriteLine("Phone Home: " + p.Phone_h + " | Phone Work: " + p.Phone_w);
                Console.WriteLine("Email: " + p.Email);
                Console.WriteLine("Skin: " + p.Color + " #noracist");
                Console.WriteLine("Height: " + p.Height + " | Weight: " + p.Weight);
                Console.WriteLine("Blood: " + p.Blood +" | Eye: " + p.Eye);
                Console.WriteLine("Hair: " + p.Hair +" | Haircolor: " + p.Hair_color);
                Console.WriteLine("======================================");
                Console.WriteLine();
            }
        }
    }
}
