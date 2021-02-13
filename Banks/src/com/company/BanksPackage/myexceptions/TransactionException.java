package com.company.BanksPackage.myexceptions;

public class TransactionException extends MyException {
    public static String message = "Transaction error!";

    public TransactionException(){}

    public TransactionException(String message){
        this.message = this.message + message;
    }

    @Override
    public String getMessage(){ return message; }
}
