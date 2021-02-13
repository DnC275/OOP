package com.company.BanksPackage;

public abstract class BankAccountBuilder<T extends BankAccountBuilder> {
    protected double startBalance = 0;
    protected Client accountHolder = null;
    private int bankId;

    BankAccountBuilder(){}

    int getBankId(){ return bankId; }

    BankAccountBuilder(BankAccountBuilder accountBuilder){
        this.startBalance = accountBuilder.startBalance;
        this.accountHolder = accountBuilder.accountHolder;
    }

    public T SetStartBalance(double balance){
        this.startBalance = balance;
        return (T)this;
    }

    T SetAccountHolder(Client client){
        accountHolder = client;
        return (T)this;
    }

    T setBankId(int bankId){
        this.bankId = bankId;
        return (T)this;
    }

    abstract BankAccountBuilder getCopy();

    abstract BankAccount GetResult(String accountNumber);
}
