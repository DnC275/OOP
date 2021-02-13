package com.company.BanksPackage.myexceptions;

public class AccountCannotCancelTransactionException extends MyException {
    String firstAccountNumber;
    String secondAccountNumber;

    public AccountCannotCancelTransactionException(String first, String second){
        firstAccountNumber = first;
        secondAccountNumber = second;
    }

    @Override
    public String getMessage(){
        return "One of the accounts " + firstAccountNumber + " and " + secondAccountNumber + " cannot cancel this transaction";
    }
}
