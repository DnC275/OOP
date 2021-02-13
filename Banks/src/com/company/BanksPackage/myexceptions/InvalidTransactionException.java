package com.company.BanksPackage.myexceptions;

public class InvalidTransactionException extends TransactionException {
    private String accountNumber;
    private double limitAmount;

    public InvalidTransactionException(String accountNumber, double limitAmount){
        this.accountNumber = accountNumber;
        this.limitAmount = limitAmount;
    }

    @Override
    public String getMessage(){
        return "Transactions for the " + accountNumber + " account are limited to " + limitAmount;
    }
}
