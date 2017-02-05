using System;
using System.Collections.Generic;
using System.Diagnostics;
using Sockets;
using People;

namespace Addressbook {
    enum ServerCommand {
        NONE,
        FINDPERSONS,
        GETALLPERSONS,
        ADDPERSON,
        DELETEPERSON
    }

    enum ClientInfo {
        NOMOREDATA,
        MOREDATA
    }

    class ControllerClient {
        private ClientSocket client;
        private View view;
        private string host;
        private int port;

        public ControllerClient(string _host, int _port) {
            this.host = _host;
            this.port = _port;
            // Zugriff auf die View
            this.view = new View();
        }

        // Hiermit wird der Client gestartet
        public int Start() {
            // Hier erfolgt die Interaktion mit dem Benutzer
            // Die Ausgaben können in einem View-Objekt erfolgen

            int eingabe = 0;

            // Menü ausgeben und Auswahl treffen
            eingabe = Menu();

            switch (eingabe) {
                // Suche Personen
                case 1:
                    suchePersonen();
                    break;

                // Hole Adressbuch
                case 2:
                    getWholeAddressbook();
                    break;

                case 9:
                    break;

                case 10:
                    view.Debug();
                    break;
                default:
                    break;
            } // Ende switch

            return eingabe;
        }

        private int Menu() {
            int auswahl = 0;

            this.view.Refresh(ViewMode.Menu_Main);

            // Auswahl lesen
            do {
                auswahl = Convert.ToInt32(Console.ReadLine());
            } while (auswahl < 1 || auswahl > 11);

            Console.WriteLine();

            // auswahl zurück liefern
            return auswahl;
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
