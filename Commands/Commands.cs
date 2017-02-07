namespace Commands {
    enum ServerCommand {
        None,
        GetServerInformation,
        ModifyPerson,
        FindPersons,        //Search complete String-Line with String a; a.Contains(search)
        GetAllPersons,      //Send all persons to Client
        AddPerson,          //creates a person
        DeletePerson,       //deletes with generated ID
        DeleteIDs,          //delete all sent IDs
        DeleteAllPersons    //deleteEVERYTHING
    }

    enum ClientInfo {
        NoMoreData,
        MoreData
    }

    enum ServerStatus {
        Offline,
        Busy,
        Online
    }
}