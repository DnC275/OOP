package com.company.BanksPackage.myexceptions;

public class OtherBankAccountBuilder extends MyException{
    @Override
    public String getMessage(){
        return "This bank account builder taken from another bank";
    }
}
