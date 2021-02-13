package com.company.BanksPackage.myexceptions;

import com.company.BanksPackage.Transaction;

public class AlreadyCanceledTransactionException extends MyException {
    @Override
    public String getMessage(){
        return "This transaction has already been canceled";
    }
}
