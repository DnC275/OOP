package com.company.BanksPackage.myexceptions;

import com.company.BanksPackage.Transaction;

public class DepositNotClosedException extends TransactionException {
    @Override
    public String getMessage(){
        return "Deposit doesn't closed";
    }
}
