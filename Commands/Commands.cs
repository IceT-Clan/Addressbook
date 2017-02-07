namespace Commands {
    enum ServerCommand {
        NONE,
        GetServerInformation,
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

    enum ServerStatus {
        Offline,
        Busy,
        Online
    }
}