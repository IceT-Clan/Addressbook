﻿using System;
using System.Collections.Generic;
using System.IO;
using People;
using System.Text.RegularExpressions;
using Commands;

namespace Addressbook { 
    // Diese Klasse stellt das Datenmodell für das Adressbuch
    // und notwendige Methoden für die Datenverarbeitung
    // zur Verfügung
    class Model {
        private String path;
        private List<Person> persons;
        private Char seperator;

        public List<Person> Persons {
            get { return this.persons; }
            set { this.persons = value; }
        }

        public void AddPerson(Person person) => this.persons.Add(person);
        public void RemovePerson(Person person) => this.persons.RemoveAll(x => x == person);
        public void AddPersonList(List<Person> persons) => this.persons.AddRange(persons);

        public Model(String path, Char seperator = ',') {
            this.persons = new List<Person>();
            this.path = path;
            this.seperator = seperator;

            ReadAddressbook();
        }

        ~Model() { WriteAddressBook(); }

        public List<Person> Search(String pattern, SearchType searchType = SearchType.FIXED_STRING) {
            List<Person> results = new List<Person>();
            switch (searchType) {
                case SearchType.REGEX:
                    results = Search_regex(pattern);
                    break;
                case SearchType.FIXED_STRING:
                    results = Search_fixed_string(pattern);
                    break;
            }
            return results;
        }
        private List<Person> Search_fixed_string(String pattern) => this.persons.FindAll(x => x.ToList().Exists(y => y.ToString().Contains(pattern)));
        private List<Person> Search_regex(String pattern) => this.persons.FindAll(x => x.ToList().Exists(y => new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(y.ToString())));


        private void WriteAddressBook() {
            Console.WriteLine("Saving Addressbook...");
            List<String> lines = new List<String>();

            this.persons.ForEach(delegate (Person person) { lines.Add(person.ToString(this.seperator)); });
            File.WriteAllLines(this.path, lines, System.Text.Encoding.UTF8);
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
            Int32 totalLines = File.ReadAllLines(this.path).Length;

            Console.WriteLine(String.Format("Found {0} lines", totalLines));
            DateTime startTime = DateTime.Now;
            Int32 lineCounter = 0;
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
                    }
                }
            }
            Console.WriteLine(String.Format("\nFound {0} entries in {1}", this.persons.Capacity, new TimeSpan(DateTime.Now.Ticks - startTime.Ticks).ToString("c")));
            return true;
       }
    }
}
