using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sockets;
using People;
using Commands;

namespace Addressbook {
    class ControllerClient {
        private ClientSocket client;
        private View view;
        private String host;
        private Int32 port;
        private Char seperator;

        public ControllerClient(string host, int port) {
            this.host = host;
            this.port = port;
            this.view = new View();
        }

        public void Start() {
            this.view.Refresh(ViewMode.Title);
            Console.ReadKey(false);

            // check if server exists and return info
            this.view.Refresh(ViewMode.Connecting);
            Console.WriteLine(String.Format("Connecting to {0} on port {1}", this.host, this.port));
            this.client = new ClientSocket(this.host, this.port);
            if (!this.client.Connect()) {
                Console.WriteLine(String.Format("Server at {0}:{1} is not available", this.host, this.port));
                return;
            }
            GetServerInformation();
            this.client.Close();

            Boolean notExit = true;
            while (notExit) {
                this.view.Refresh(ViewMode.Menu_Main);
                String input = Console.ReadLine();
                switch (input) {
                    case "1":
                        SearchPerson();
                        Console.ReadKey();
                        break;
                    case "2":
                        GetWholeAddressbook();
                        Console.ReadKey();
                        break;
                    case "3":
                        AddPerson();
                        Console.ReadKey();
                        break;
                    case "9":
                        notExit = true;
                        break;
                    case "10":
                        this.view.Debug();
                        break;
                    default:
                        break;
                }
            }
        }

        private void GetServerInformation() {
            this.client.Write((int)ServerCommand.GetServerInformation);
            if (this.client.Read() != (int)ServerStatus.Online) return;

            Char seperator = (Char)this.client.Read();
            Int32 entries = this.client.Read();

            Console.WriteLine(String.Format("Server uses {0} as seperator", seperator));
            Console.WriteLine(String.Format("Server has {0} total entries", entries));

            this.seperator = seperator;
        }

        private void AddPerson() {
            Person person = this.view.Add_Entry();
        }

        private void SearchPerson() {
            // Suchbegriff abfragen
            Console.Write("Search> ");
            string pattern = Console.ReadLine();


            // Hier müsste eine Ausnahmebehandlung erfolgen
            // falls keine Verbindung möglich ist
            this.client = new ClientSocket(this.host, this.port);

            // Verbindung mit Server herstellen
            this.client.Connect();

            // Kommando senden
            this.client.Write((int)ServerCommand.FindPersons);

            // search type


            // Suchstring senden
            this.client.Write(pattern);

            // Anzahl gefundener Personen lesen
            int entryCount = this.client.Read();

            Console.WriteLine("Found {0} entries", entryCount);

            if (entryCount > 0) {
                List<Person> result = new List<Person>();
                for (int i = 0; i < entryCount; i++) result.Add(new Person(this.client.ReadLine(), this.seperator));
                this.view.Data = result;
                this.view.Refresh(ViewMode.MultipleEntries);
            }
            this.client.Close();
        }

        private void GetWholeAddressbook() {
            this.client = new ClientSocket(this.host, this.port);
            this.client.Connect();

            this.client.Write((int)ServerCommand.GetAllPersons);

            Int32 entryCount = this.client.Read();

            if (entryCount > 0) {
                List<Person> result = new List<Person>();
                for (int i = 0; i < entryCount; i++) result.Add(new Person(this.client.ReadLine(), this.seperator));
                this.view.Data = result;
                this.view.Refresh(ViewMode.MultipleEntries);
            }
            this.client.Close();
        }
    }
}
