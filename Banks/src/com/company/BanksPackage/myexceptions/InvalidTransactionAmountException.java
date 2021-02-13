package com.company.BanksPackage.myexceptions;

public class InvalidTransactionAmountException extends TransactionException {
    @Override
    public String getMessage(){
        return "Transaction amount should be positive";
    }
}
