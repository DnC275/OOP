package com.company.BanksPackage;

import java.time.*;

public class DepositBuilder extends BankAccountBuilder<DepositBuilder> {
    private double interestOnBalance = 3;
    private LocalDate closingDate = null;

    DepositBuilder(){}

    DepositBuilder(DepositBuilder accountBuilder){
        super(accountBuilder);
        this.interestOnBalance = accountBuilder.interestOnBalance;
    }

    public DepositBuilder setOpeningPeriod(int months){
        closingDate = LocalDate.now().plusMonths(months);
        return this;
    }

    @Override
    public DepositBuilder SetStartBalance(double balance){
        super.SetStartBalance(balance);
        if (balance < 50000)
            interestOnBalance = 3;
        else if (balance < 100000)
            interestOnBalance = 3.5;
        else
            interestOnBalance = 4;
        return this;
    }

    @Override
    DepositBuilder getCopy(){
        return new DepositBuilder(this);
    }

    @Override
    Deposit GetResult(String accountNumber){
        return new Deposit(getBankId(), accountNumber, accountHolder, startBalance, interestOnBalance, closingDate);
    }
}
