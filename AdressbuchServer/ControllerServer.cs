﻿using System;
using System.Collections.Generic;
using Sockets;
using People;

namespace Addressbook {
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

        public ControllerServer(int port, string addressbook) {
            this.model = new Model(addressbook);
            this.server = new ServerSocket(port);
        }

        public void start() {
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
            string separator = ";";

            clientSocket.Write(persons.Count);

            if (persons.Count > 0) {
                foreach (Person p in persons) {
                    string data = "";
                    data += p.Name + separator;
                    data += p.Address + separator;
                    data += p.Birth_data.Date.ToShortDateString();
                    data += p.Phone_h + separator;
                    data += p.Phone_w + separator;
                    data += p.Email + separator;
                    data += p.Color + separator;
                    data += p.Height + separator;
                    data += p.Weight + separator;
                    data += p.Blood + separator;
                    data += p.Eye + separator;
                    data += p.Hair + separator;
                    data += p.Hair_color + separator;
                    clientSocket.Write(data + "\n");
                }
            }
        }

        private void AddNewEntry(ClientSocket clientSocket) {

        }

        private void RemEntry(ClientSocket clientSocket) {

        }
    }
}
