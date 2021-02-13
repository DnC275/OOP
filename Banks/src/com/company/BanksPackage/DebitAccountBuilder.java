package com.company.BanksPackage;

public class DebitAccountBuilder extends BankAccountBuilder<DebitAccountBuilder>{
    private double interestOnBalance = 3.5;

    DebitAccountBuilder(){}

    DebitAccountBuilder(DebitAccountBuilder accountBuilder){
        super(accountBuilder);
        this.interestOnBalance = accountBuilder.interestOnBalance;
    }

    DebitAccountBuilder SetInterestOnBalance(double interestOnBalance){
        this.interestOnBalance = interestOnBalance;
        return this;
    }

    @Override
    DebitAccountBuilder getCopy(){
        return new DebitAccountBuilder(this);
    }

    @Override
    DebitAccount GetResult(String accountNumber){
        return new DebitAccount(getBankId(), accountNumber, accountHolder, startBalance, interestOnBalance);
    }
}
