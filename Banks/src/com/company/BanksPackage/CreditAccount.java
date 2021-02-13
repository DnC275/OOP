package com.company.BanksPackage;

import com.company.BanksPackage.myexceptions.*;

public class CreditAccount extends BankAccount{
    private double negativeLimit;
    private double commission;

    CreditAccount(int bankId, String accountNumber, Client accountHolder, double balance, double negativeLimit, double commission){
        super(bankId, accountNumber, accountHolder, balance);
        this.negativeLimit = negativeLimit;
        this.commission = commission;
    }

    @Override
    boolean checkOnCancelTransaction(Transaction transaction){
        return true;
    }

    @Override
    AccountType GetType() { return AccountType.Credit; }

    @Override
    double WithdrawalAmount(double amount){
        return amount + (GetBalance() >= 0 ? 0 : commission);
    }
}
