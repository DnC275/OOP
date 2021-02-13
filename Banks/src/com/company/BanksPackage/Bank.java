package com.company.BanksPackage;

import com.company.BanksPackage.myexceptions.*;
import org.jetbrains.annotations.*;

import java.util.*;

public class Bank {
    private int accountCommonId = 0;
    private final int id;
    private String name;
    private double transactionAmountLimit;
    private final Map<Integer, BankAccount> existingAccounts = new HashMap<>();
    private final Map<AccountType, BankAccountBuilder> accountTemplates;

    int GetId() { return id; }

    double getTransactionAmountLimit() { return transactionAmountLimit; }

    public String getName(){ return name; }

    Bank(int id, String name, double transactionAmountLimit, Map<AccountType, BankAccountBuilder> accountTemplates){
        this.id = id;
        this.name = name;
        this.transactionAmountLimit = transactionAmountLimit;
        this.accountTemplates = new HashMap<>(accountTemplates);
    }

    public DebitAccountBuilder getDebitAccountBuilder(){
        return ((DebitAccountBuilder)accountTemplates.get(AccountType.Debit)).getCopy().setBankId(id);
    }

    public CreditAccountBuilder getCreditAccountBuilder(){
        return ((CreditAccountBuilder)accountTemplates.get(AccountType.Credit)).getCopy().setBankId(id);
    }

    public DepositBuilder getDepositBuilder(){
        return ((DepositBuilder)accountTemplates.get(AccountType.Deposit)).getCopy().setBankId(id);
    }

    public String createAccount(Client client, BankAccountBuilder builder) throws MyException{
        if (builder == null)
            throw new NullPointerException();
        if (builder.getBankId() != id)
            throw new OtherBankAccountBuilder();
        accountCommonId++;
        builder.SetAccountHolder(client);
        BankAccount account = builder.GetResult(CountAccountNumber(id, accountCommonId));
        existingAccounts.put(account.GetId(), account);
        return account.GetNumber();
    }

    boolean checkAccountExistence(Integer accountId){
        return existingAccounts.containsKey(accountId);
    }

    boolean checkClientTrusted(Integer accountId){
        return existingAccounts.get(accountId).CheckAddressAndPassport();
    }

    boolean verifyOwnership(Client accountHolder, Integer accountId){
        return existingAccounts.get(accountId).getAccountHolder() == accountHolder;
    }

    void chargeMoney(Integer accountId, Transaction transaction){
        existingAccounts.get(accountId).Charge(transaction);
    }

    void withdrawMoney(Integer accountId, Transaction transaction) throws TransactionException{
        existingAccounts.get(accountId).Withdraw(transaction);
    }

    Client getClient(int accountId){
        return existingAccounts.get(accountId).getAccountHolder();
    }

    boolean checkAccountForCancelTransaction(int accountId, Transaction transaction){
        return existingAccounts.get(accountId).checkOnCancelTransaction(transaction);
    }

    void cancelTransaction(int accountId, Transaction transaction) throws MyException{
        existingAccounts.get(accountId).cancelTransaction(transaction);
    }

    private @NotNull
    String CountAccountNumber(int bankId, int accountId){
        String firstPart = String.valueOf(bankId);
        while (firstPart.length() < 4){
            firstPart = "0" + firstPart;
        }
        String secondPart = String.valueOf(accountId);
        while (secondPart.length() < 4){
            secondPart = "0" + secondPart;
        }
        return firstPart + secondPart;
    }

    public static class BankBuilder{
        private int id;
        private String name = null;
        private double transactionAmountLimit;
        private final Map<AccountType, BankAccountBuilder> accountTemplates = new HashMap<>();

        public BankBuilder(){
            accountTemplates.put(AccountType.Debit, new DebitAccountBuilder());
            accountTemplates.put(AccountType.Credit, new CreditAccountBuilder());
            accountTemplates.put(AccountType.Deposit, new DepositBuilder());
        }

        void setId(int id) {
            this.id = id;
            name = (name == null) ? ("Bank" + id) : name;
        }

        public BankBuilder SetName(String name){
            this.name = name;
            return this;
        }

        public BankBuilder SetDebitAccountConditions(double interestOnBalance){
            ((DebitAccountBuilder)accountTemplates.get(AccountType.Debit)).SetInterestOnBalance(interestOnBalance);
            return this;
        }

        public BankBuilder SetCreditAccountConditions(double commission){
            ((CreditAccountBuilder)accountTemplates.get(AccountType.Credit)).SetCommission(commission);
            return this;
        }

        public BankBuilder SetTransactionAmountLimit(double transactionAmountLimit) {
            this.transactionAmountLimit = transactionAmountLimit;
            return this;
        }

        Bank GetResult(){
            return new Bank(id, name, transactionAmountLimit, accountTemplates);
        }
    }
}
