using System;
using System.Collections.Generic;
using Sockets;
using People;
using Commands;

namespace Addressbook {
    class Log {
        public static void Write(String arg) => Console.Write(String.Format("[{0}] {1}", DateTime.Now.ToShortTimeString(), arg));
    }

    class ControllerServer {
        private Model model;
        private ServerSocket server;

        public ControllerServer(int port, string addressbook) {
            this.model = new Model(addressbook);
            this.server = new ServerSocket(port);
        }


        public void Start() {
            bool hasNotStopped = true;
            Console.WriteLine("Server started");

            // Server kann nicht gestoppt werden
            while (hasNotStopped) {
                // ServerSocket in listen-Modus
                Console.WriteLine("Waiting for connection...");

                ClientSocket client = new ClientSocket(this.server.Accept());
                Console.CursorTop--;
                Console.WriteLine("Connection established");


                // Der folgende Teil würde in einen separaten Thread ausgelagert,
                // um den Server wieder für neue Verbindungen zu öffnen
                // Dieser Thread würde den Client-Socket als Parameter
                // für die weitere Kommnikation erhalten

                // Client-Socket liest Kommando vom Client
                ServerCommand command = (ServerCommand)client.Read();

                // Kommando wird ausgewertet
                Log.Write(String.Format("Got Command {0}", command.ToString()));
                switch (command) {
                    case ServerCommand.None:
                        break;
                    case ServerCommand.FindPersons:
                        Search(client);
                        break;
                    case ServerCommand.GetAllPersons:
                        GetAllEntries(client);
                        break;
                    case ServerCommand.AddPerson:
                        AddNewEntry(client);
                        break;
                    case ServerCommand.DeletePerson:
                        RemEntry(client);
                        break;
                    case ServerCommand.GetServerInformation:
                        SendServerInformation(client);
                        break;
                    default:
                        break;
                } // Ende switch

                client.Close();
                client.Dispose();
                Log.Write("Connection closed");
                Console.WriteLine("");
            } // Ende while

        }

        private void Search(ClientSocket clientSocket) {
            // Lese Suchstring vom Client
            string pattern = clientSocket.ReadLine();

            // Speichere die Ergebnisse in einer Liste
            List<Person> results = this.model.Search(pattern);

            // Sende Client die Anzahl der gefundenen Personen
            clientSocket.Write(results.Count);

            // Sende nun die Personendaten
            if (results.Count > 0) {
                foreach (Person person in results) {
                    string data = person.ToString();

                    // Testausgabe
                    Console.WriteLine(data);
                    clientSocket.Write(data + "\n");
                }
            }
        }

        private void GetAllEntries(ClientSocket clientSocket) {
            Log.Write("Sending all entries");
            List<Person> persons = this.model.GetAllEntries();
            Char seperator = ';';

            clientSocket.Write(persons.Count);

            if (persons.Count > 0) {
                foreach (Person person in persons) {
                    string data = person.ToString(seperator);
                    clientSocket.Write(data + '\n');
                }
            }
        }

        private void SendServerInformation(ClientSocket clientSocket) {
            Log.Write("Sending server information");
            Char seperator = ';';
            clientSocket.Write((int)ServerStatus.Online);
            clientSocket.Write(seperator);
            clientSocket.Write(this.model.GetAllEntries().Count);

        }

        private void AddNewEntry(ClientSocket clientSocket) => this.model.AddPerson(new Person(clientSocket.ReadLine()));

        private void RemEntry(ClientSocket clientSocket) {

        }
    }
}
