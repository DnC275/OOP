package com.company.BanksPackage;

public class CreditAccountBuilder extends BankAccountBuilder<CreditAccountBuilder>{
    private double negativeLimit;
    private double commission;

    CreditAccountBuilder(){}

    CreditAccountBuilder(CreditAccountBuilder accountBuilder){
        super(accountBuilder);
        this.negativeLimit = accountBuilder.negativeLimit;
        this.commission = accountBuilder.commission;
    }

    CreditAccountBuilder SetNegativeLimit(double limit){
        negativeLimit = -limit;
        return this;
    }

    CreditAccountBuilder SetCommission(double commission){
        this.commission = commission;
        return this;
    }

    @Override
    CreditAccountBuilder getCopy(){
        return new CreditAccountBuilder(this);
    }

    @Override
    CreditAccount GetResult(String accountNumber){
        return new CreditAccount(getBankId(), accountNumber, accountHolder, startBalance, negativeLimit, commission);
    }
}
