using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using __ServerSocket__;
using __ClientSocket__;

namespace Adressbuch {
    enum ServerCommand {
        NONE,
        MODIFYPERSON,
        FINDPERSONS,        //Search complete String-Line with String a; a.Contains(search)
        GETALLPERSONS,      //Send all persons to Client
        ADDPERSON,          //creates a person
        DELETEPERSON,       //deletes with generated ID
        DELETEIDS,          //delete all sent IDs
        DELETEALLPERSONS    //deleteEVERYTHING
    }

    enum ClientInfo {
        NOMOREDATA,
        MOREDATA
    }

    class ControllerServer {
        private Model model;
        private ServerSocket server;

        public ControllerServer(int _port, string _addrbk_file) {
            model = new Model(_addrbk_file);
            server = new ServerSocket(_port);
        }

        public void start() {
            Console.WriteLine("Server gestartet!");

            // Server kann nicht gestoppt werden
            while (true) {
                // ServerSocket in listen-Modus
                ClientSocket client = new ClientSocket(server.accept());

                Console.WriteLine("Verbindung hergestellt!");

                // Der folgende Teil würde in einen separaten Thread ausgelagert,
                // um den Server wieder für neue Verbindungen zu öffnen
                // Dieser Thread würde den Client-Socket als Parameter
                // für die weitere Kommnikation erhalten

                // Client-Socket liest Kommando vom Client
                ServerCommand command = (ServerCommand)client.read();

                // Kommando wird ausgewertet
                switch (command) {
                    case ServerCommand.NONE:
                        break;

                    case ServerCommand.FINDPERSONS:
                        search(client);
                        break;

                    case ServerCommand.GETALLPERSONS:
                        getAllEntries(client);
                        break;

                    case ServerCommand.ADDPERSON:
                        addNewEntry(client);
                        break;

                    case ServerCommand.DELETEPERSON:
                        remEntry(client);
                        break;

                    default:
                        break;
                } // Ende switch

                client.close();
                client = null;
                Console.WriteLine("Verbindung geschlossen!");
                Console.WriteLine("=======================");

            } // Ende while

        }

        private void search(ClientSocket _client) {
            // Lese Suchstring vom Client
            string suchbegriff = _client.readLine();

            // Speichere die Ergebnisse in einer Liste
            List<Person> ergebnis = model.search(suchbegriff);

            // Sende Client die Anzahl der gefundenen Personen
            _client.write(ergebnis.Count);

            // Sende nun die Personendaten
            if (ergebnis.Count > 0) {
                foreach (Person p in ergebnis) {
                    string data = p.ToString();

                    // Testausgabe
                    Console.WriteLine(data);
                    _client.write(data + "\n");
                }
            }
        }

        private void getAllEntries(ClientSocket _client) {

        }

        private void addNewEntry(ClientSocket _client) {

        }

        private void remEntry(ClientSocket _client) {

        }
    }
}
