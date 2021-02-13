package com.company.BanksPackage.myexceptions;

public class InsufficientFundsForTransactionException extends TransactionException{
    String accountNumber;

    public InsufficientFundsForTransactionException(String accountNumber) { this.accountNumber = accountNumber; }

    @Override
    public String getMessage(){
        return "Insufficient funds in the account " + accountNumber;
    }
}
