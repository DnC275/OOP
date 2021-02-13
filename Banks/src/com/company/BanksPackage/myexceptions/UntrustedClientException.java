package com.company.BanksPackage.myexceptions;

public class UntrustedClientException extends TransactionException {
    private String accountNumber;
    private double limitAmount;

    public UntrustedClientException(String accountNumber, double limitAmount){
        this.accountNumber = accountNumber;
        this.limitAmount = limitAmount;
    }

    @Override
    public String getMessage(){
        return "Transactions for the " + accountNumber + " account are limited to " + limitAmount;
    }
}
