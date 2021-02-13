package com.company.BanksPackage.myexceptions;

import com.company.BanksPackage.Client;

public class CancelTransactionException extends MyException {
    String firstName;
    String secondName;

    public CancelTransactionException(Client client){
        firstName = client.GetFirstName();
        secondName = client.GetSecondName();
    }

    @Override
    public String getMessage(){
        return "Client " + firstName + " " + secondName + " is not recipient of this transaction";
    }
}
