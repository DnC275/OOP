package com.company.BanksPackage.myexceptions;

public class TransactionNonexistentBankException extends TransactionException {
    private String accountNumber;

    public TransactionNonexistentBankException(String accountNumber) { this.accountNumber = accountNumber; }

    @Override
    public String getMessage(){
        return message + " Bank whit the account " + accountNumber + " doesn't exists";
    }
}
