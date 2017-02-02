using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adressbuch {
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

        public List<Person> Search(string pattern, SearchType searchType=SearchType.FIXED_STRING) {
            List<Person> results = new List<Person>();
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
                    results = Search_fixed_string("ss");
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }

            return results;
        }

        private List<Person> Search_fixed_string(string pattern) {
            List<Person> result = new List<Person>();
            foreach (Person person in this.persons) {
                if (person.Name.Contains(pattern) ||
                    false) {
                    result.Add(person);
                } 
            }
            return result;
        }

        public List<Person> GetAllEntries() => this.persons;
        public Person GetPersonFromID(int id) => this.persons[id];

        private void ReadAddressbook() {
            if (!System.IO.File.Exists(this.path)) {
                Console.WriteLine("No" + this.path + "found!\n");
                File.Create(this.path);
            }

            using (StreamReader reader = new StreamReader(this.path)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    this.persons.Add(new Person(line));
                }
            }

        }
    }
}
