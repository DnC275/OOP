package com.company.BanksPackage.myexceptions;

public class BankAlreadyExistsException extends MyException{
    @Override
    public String getMessage(){
        return "Bank with the same name already exists";
    }
}
