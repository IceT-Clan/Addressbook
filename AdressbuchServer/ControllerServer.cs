﻿using System;
using System.Collections.Generic;
using Sockets;
using People;
using Commands;

namespace Addressbook {
    class ControllerServer {
        private Model model;
        private ServerSocket server;

        public ControllerServer(int port, string addressbook) {
            this.model = new Model(addressbook);
            this.server = new ServerSocket(port);
        }

        public void Start() {
            bool hasNotStopped = true;
            Console.WriteLine("Server gestartet!");

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
                switch (command) {
                    case ServerCommand.NONE:
                        break;

                    case ServerCommand.FINDPERSONS:
                        Search(client);
                        break;

                    case ServerCommand.GETALLPERSONS:
                        GetAllEntries(client);
                        break;

                    case ServerCommand.ADDPERSON:
                        AddNewEntry(client);
                        break;

                    case ServerCommand.DELETEPERSON:
                        RemEntry(client);
                        break;

                    default:
                        break;
                } // Ende switch

                client.Close();
                client.Dispose();
                Console.WriteLine("Verbindung geschlossen!");
                Console.WriteLine("=======================");

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

        private void AddNewEntry(ClientSocket clientSocket) {

        }

        private void RemEntry(ClientSocket clientSocket) {

        }
    }
}
