package com.company.BanksPackage;

import com.company.BanksPackage.myexceptions.AlreadyCanceledTransactionException;
import com.company.BanksPackage.myexceptions.MyException;
import com.company.BanksPackage.myexceptions.TransactionException;

import java.util.*;

abstract class BankAccount {
    private final int id;
    private final String accountNumber;
    private final Client accountHolder;
    protected double balance;
    private int bankId;
    protected List<Transaction> completedTransactions;
    protected List<Transaction> canceledTransactions;

    int GetId() { return id; }

    int getBankId() { return bankId; }

    String getAccountNumber() { return accountNumber; }

    Client getAccountHolder() { return accountHolder; }

    public String GetNumber() { return accountNumber; }

    abstract AccountType GetType();

    public double GetBalance() { return balance; }

    BankAccount(int bankId, String accountNumber, Client accountHolder, double balance){
        id = Integer.parseInt(accountNumber.substring(4));
        this.bankId = bankId;
        this.accountNumber = accountNumber;
        this.accountHolder = accountHolder;
        this.balance = balance;
        completedTransactions = new LinkedList<>();
        canceledTransactions = new LinkedList<>();
    }

    public boolean CheckAddressAndPassport(){
        if (accountHolder.GetAddress() != null && accountHolder.GetPassportNumber() != null)
            return true;
        return false;
    }

    void Withdraw(Transaction transaction) throws TransactionException{
        balance -= WithdrawalAmount(transaction.getAmount());
        completedTransactions.add(transaction);
    }

    void Charge(Transaction transaction){
        System.out.println(balance);
        balance += transaction.getAmount();
        System.out.println(balance);
        completedTransactions.add(transaction);
    }

    abstract double WithdrawalAmount(double amount) throws TransactionException;

    abstract boolean checkOnCancelTransaction(Transaction transaction);

    void cancelTransaction(Transaction transaction) throws MyException {
        if (canceledTransactions.contains(transaction))
            throw new AlreadyCanceledTransactionException();
        if (transaction.getSenderNumber().equals(getAccountNumber())){
            balance += transaction.getAmount();
        }
        else{
            balance -= transaction.getAmount();
        }
        completedTransactions.remove(transaction);
        canceledTransactions.add(transaction);
    }
}