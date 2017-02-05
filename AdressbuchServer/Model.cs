using System;
using System.Collections.Generic;
using System.IO;
using People;
using System.Diagnostics;

namespace Addressbook {
    enum SearchType {
        BASIC_REGEX,
        REGEX,
        PERL_REGEX,
        FIXED_STRING
    }

    // Diese Klasse stellt das Datenmodell für das Adressbuch
    // und notwendige Methoden für die Datenverarbeitung
    // zur Verfügung
    class Model {
        private string path;
        private List<Person> persons;

        public Model(string path) {
            this.persons = new List<Person>();
            this.path = path;

            ReadAddressbook();
        }

        public List<Person> Search(string pattern, SearchType searchType = SearchType.FIXED_STRING) {
            List<Person> results = new List<Person>();
#pragma warning disable CS0162 // Unreachable code detected
            switch (searchType) {
                case SearchType.BASIC_REGEX:
                    throw new NotImplementedException();
                    break;
                case SearchType.REGEX:
                    throw new NotImplementedException();
                    break;
                case SearchType.PERL_REGEX:
                    throw new NotImplementedException();
                    break;
                case SearchType.FIXED_STRING:
                    results = Search_fixed_string(pattern);
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
#pragma warning restore CS0162 // Unreachable code detected

            return results;
        }

        private List<Person> Search_fixed_string(string pattern) {
            List<Person> result = new List<Person>();
            foreach (Person person in this.persons) {
                foreach (Object obj in person.ToList()) {
                    if (obj.ToString().Contains(pattern)) {
                        result.Add(person);
                    }
                }
            }

            return result;
        }

        public List<Person> GetAllEntries() => this.persons;
        public Person GetPersonFromID(int id) => this.persons[id];

        private Boolean ReadAddressbook() {
            Console.WriteLine("Reading Addressbook...");
            if (!File.Exists(this.path)) {
                Console.WriteLine(String.Format("{0} not found.", this.path));
                return false;
            }

            Console.WriteLine("Get Addressbook line count...");
            Int32 totalLines = 0;
            using (StreamReader reader = new StreamReader(this.path)) {
                while ((reader.ReadLine()) != null) {
                    totalLines++;
                }
            }
            Console.WriteLine(String.Format("Found {0} lines", totalLines));
            DateTime startTime = DateTime.Now;
            Int32 lineCounter = 0;
            Int32 goodCounter = 0;
            using (StreamReader reader = new StreamReader(this.path)) {
                String line;
                while ((line = reader.ReadLine()) != null) {
                    lineCounter++;
                    Console.WriteLine(String.Format("Reading line {0} of {1} {2}%", 
                        lineCounter.ToString().PadLeft(totalLines.ToString().Length), 
                        totalLines.ToString(),
                        ((float)lineCounter / totalLines * 100).ToString().PadRight(5).Substring(0, 5).TrimEnd(' ')));
                    Console.CursorTop--;
                    if (!line.StartsWith("#") && line != "") {
                        this.persons.Add(new Person(line));
                        goodCounter++;
                    }
                }
            }
            Console.WriteLine(String.Format("\nFound {0} entries in {1}", goodCounter, new TimeSpan(DateTime.Now.Ticks - startTime.Ticks).ToString("c")));
            return true;
       }
    }
}
