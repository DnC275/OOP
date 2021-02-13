package com.company.BanksPackage.myexceptions;

import com.company.BanksPackage.Client;

public class NotYourAccountException extends TransactionException {
    String clientFullName;
    String accountNumber;

    public NotYourAccountException(Client client, String accountNumber){
        clientFullName = client.GetFirstName() + client.GetSecondName();
        this.accountNumber = accountNumber;
    }

    @Override
    public String getMessage(){
        return "Account " + accountNumber + " doesn't belong to " + clientFullName;
    }
}
