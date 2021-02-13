package com.company.BanksPackage.myexceptions;

public class TransactionNonexistentAccountException extends TransactionException{
    private String accountNumber;

    public TransactionNonexistentAccountException(String accountNumber){
        this.accountNumber = accountNumber;
    }

    @Override
    public String getMessage(){
        return message + " " + accountNumber + " account doesn't exist";
    }
}
