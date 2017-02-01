using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch
{
    // Diese Klasse stellt das Datenmodell für das Adressbuch
    // und notwendige Methoden für die Datenverarbeitung
    // zur Verfügung
    class Model
    {
        private string _path;
        // Objektvariable für Zugriff auf Liste
        private List<Person> personen;

        public Model(string path)
        {
            // Leere Liste erstellen
            personen = new List<Person>();

            // Datensätze aus adressbuch.txt lesen,
            // Person-Objekte erstellen und
            // der Liste hinzufügen

            leseAdressbuchDatei();

            _path = path;
        }

        public List<Person> search(string wert)
        {
            // leere Ergebnisliste erstellen
            List<Person> ergebnis = new List<Person>();

            foreach (Person p in personen)
            {
                if (p.Name.Contains(wert) ||
                    p.Address.Contains(wert)
                   )
                {
                    Person newPerson = new Person(
                                                  p.Name,
                                                  p.Address,
                                                  p.Birth_data,
                                                  p.Phone_h,
                                                  p.Phone_w,
                                                  p.Email,
                                                  p.Color,
                                                  p.Height,
                                                  p.Weight,
                                                  p.Blood,
                                                  p.Eye,
                                                  p.Hair,
                                                  p.Hair_color
                                                 );
                    ergebnis.Add(newPerson);
                }
            }

            return ergebnis;

        }

        public List<Person> getall()
        {
            List<Person> erg = new List<Person>();
            foreach (Person p in personen)
            {
                Person newPerson = new Person(p.Name, p.Address, p.Birth_data);
                erg.Add(newPerson);
            }
            return erg;
        }

        // Liest die Datei adressbuch.txt und erstellt Person-Objekte
        private bool leseAdressbuchDatei()
        {
            // Hiermit könnte Erfolg oder Misserfolg der
            // Methode zurückgemeldet werden
            // Besser wäre, bei Misserfolg eine Ausnahme zu werfen
            bool rc = true;

            if (System.IO.File.Exists(_path) == false)
            {
                Console.WriteLine("No"+ _path+ "found!\n");
                File.Create(_path);
            }

            // automatische Freigabe der Ressource mittels using
            using (StreamReader sr = new StreamReader(_path))
            {
                string zeile;
                // Lesen bis Dateiende, Zeile für Zeile
                while ((zeile = sr.ReadLine()) != null)
                {
                    // Person-Objekt erstellen anhand gelesener Zeile
                    Person p = convertString2Person(zeile);

                    // Person-Objekt in die Liste einfügen
                    personen.Add(p);
                }
            }

            return rc;
        }

        private Person convertString2Person(string _p)
        {
            char[] separator = { ';' };
            string[] daten = _p.Split(separator);

            // Geburtsdatum umformen, um ein DateTime-Objekt
            // zu erstellen
            char[] trenner = { '.' };
            string[] geburtsdatum = daten[3].Split(trenner);

            int tag = Convert.ToInt32(geburtsdatum[0]);
            int monat = Convert.ToInt32(geburtsdatum[1]);
            int jahr = Convert.ToInt32(geburtsdatum[2]);

            DateTime datum = new DateTime(jahr, monat, tag);

            // Person-Objekt erstellen und der Liste hinzufügen
            Person p = new Person(daten[0], daten[1], datum, daten[2], daten[3], daten[4], daten[5], Int32.Parse(daten[6]), Int32.Parse(daten[7]), daten[8], daten[9], daten[10]);

            return p;
        }

        private string convertPerson2String(Person _p)
        {
            string person = "";

            // Hier wird ein Person-Objekt in den String umgeformt

            return person;
        }


        // Schreibt die Person-Objekte in die Datei adressbuch.txt
        private bool schreibeAdressbuchDatei()
        {
            bool rc = false;

            return rc;
        }

        // Dient nur zu Testzwecken
        // Zeigt das Adressbuch auf der Server-Konsole
        private void zeigeAdressbuch()
        {
            foreach (Person p in personen)
            {
                Console.WriteLine( p.Name + " : " + p.Address + " : " + p.Birth_data.ToShortDateString());
            }
        }
    }
}
