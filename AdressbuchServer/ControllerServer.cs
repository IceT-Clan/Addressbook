using System;
using System.Collections.Generic;
using Sockets;
using People;
using Commands;

namespace Addressbook {
    class Log {
        public static void Write(String arg) => Console.WriteLine(String.Format("[{0}] {1}", DateTime.Now.ToShortTimeString(), arg));
    }

    class ControllerServer {
        private Model model;
        private ServerSocket server;
        private Char seperator = ',';

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
            Log.Write(String.Format("Searching for pattern {0}", pattern));

            // Speichere die Ergebnisse in einer Liste
            List<Person> results = this.model.Search(pattern);

            // Sende Client die Anzahl der gefundenen Personen
            clientSocket.Write(results.Count);

            results.ForEach(person => clientSocket.WriteLine(person.ToString(this.seperator)));
        }

        private void GetAllEntries(ClientSocket clientSocket) {
            Log.Write("Sending all entries");

            clientSocket.Write(this.model.GetAllEntries().Count);

            this.model.GetAllEntries().ForEach(person => clientSocket.WriteLine(person.ToString(this.seperator)));
        }

        private void SendServerInformation(ClientSocket clientSocket) {
            Log.Write("Sending server information");
            clientSocket.Write((int)ServerStatus.Online);
            clientSocket.Write(this.seperator);
            clientSocket.Write(this.model.GetAllEntries().Count);
        }

        private void AddNewEntry(ClientSocket clientSocket) => this.model.AddPerson(new Person(clientSocket.ReadLine()));

        private void RemEntry(ClientSocket clientSocket) {
            string pattern = clientSocket.ReadLine();
            List<Person> results = this.model.Search(pattern);

            clientSocket.WriteLine(results.Count.ToString());

            results.ForEach(person => clientSocket.WriteLine(person.ToString(this.seperator)));

            this.model.RemovePerson(results[clientSocket.Read()]);            
        }
    }
}
