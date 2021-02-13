package com.company.BanksPackage.myexceptions;

public class NonUniquenessBankIdException extends MyException{
    int id;

    public NonUniquenessBankIdException(int id) { this.id = id; }

    @Override
    public String getMessage(){
        return "Bank with id " + id + " already exists";
    }
}
