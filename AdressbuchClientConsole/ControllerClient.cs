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
        private string host;
        private int port;

        public ControllerClient(string host, int port) {
            this.host = host;
            this.port = port;
            this.view = new View();
        }

        // Hiermit wird der Client gestartet
        public void Start() {
            this.view.Refresh(ViewMode.Title);
            Console.ReadKey(false);

            // check if server exists and return info
            this.view.Refresh(ViewMode.Connecting);
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
                string input = Console.ReadLine();
                switch (input) {
                    case "1":
                        suchePersonen();
                        break;
                    case "2":
                        getWholeAddressbook();
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
            String entries = this.client.ReadLine();

            Console.WriteLine(String.Format("Server has {0} Entries", entries));
        }

        private void suchePersonen() {
            // Suchbegriff abfragen
            Console.Write("Suchbegriff> ");
            string suchbegriff = Console.ReadLine();


            // Hier müsste eine Ausnahmebehandlung erfolgen
            // falls keine Verbindung möglich ist
            client = new ClientSocket(host, port);
            try {

                // Verbindung mit Server herstellen
                client.Connect();

                // Kommando senden
                client.Write((int)ServerCommand.FINDPERSONS);

                // Suchstring senden
                client.Write(suchbegriff);

                // Anzahl gefundener Personen lesen
                int anzahl = client.Read();

                Console.WriteLine("Anzahl gefundener Personen: {0}", anzahl);

                if (anzahl > 0) {
                    List<Person> result = new List<Person>();

                    for (int i = 0; i < anzahl; i++) {
                        result.Add(new Person(this.client.ReadLine(), ','));
                    } // Ende for

                    // Daten anzeigen
                    this.view.Data = result;
                    this.view.Refresh(ViewMode.MultipleEntries);

                } // End if

                client.Close();

            } catch (Exception e) {
                Debug.WriteLine(e.Message);
                throw;
            }

        }

        private void getWholeAddressbook() {
            client = new ClientSocket(host, port);
            try {
                client.Connect();
                client.Write((int)ServerCommand.GETALLPERSONS);
                int count = client.Read();

                if (count >= 1) {
                    List<Person> result = new List<Person>();
                    for (int i = 0; i < count; i++) {
                        result.Add(new Person(client.ReadLine(), ','));
                    }
                    this.view.Data = result;
                    this.view.Refresh(ViewMode.MultipleEntries);
                }
                client.Close();
            } catch (Exception e) {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}
