package com.company.BanksPackage;

import com.company.BanksPackage.myexceptions.MyException;
import com.company.BanksPackage.myexceptions.TransactionException;

public class Client {
    private String firstName;
    private String secondName;
    private String address = null;
    private String passportNumber = null;

    public String GetFirstName() {
        return firstName;
    }

    public String GetSecondName() {
        return secondName;
    }

    public String GetAddress() {
        return address;
    }

    public String GetPassportNumber() {
        return passportNumber;
    }

    public void setAddress(String address){ this.address = address; }

    public void setPassportNumber(String passportNumber){ this.passportNumber = passportNumber; }

    public Client(String firstName, String secondName, String address, String passportNumber) {
        this.firstName = firstName;
        this.secondName = secondName;
        this.address = address;
        this.passportNumber = passportNumber;
    }

    public Transaction MakeTransaction(Transaction.TransactionBuilder transactionBuilder) throws TransactionException {
        return Manager.MakeTransaction(this, transactionBuilder);
    }

    public double withdrawMoney(String accountNumber, double amount) throws TransactionException {
        Transaction.TransactionBuilder transactionBuilder = new Transaction.TransactionBuilder().setSenderNumber(accountNumber).setAmount(amount);
        Manager.MakeTransaction(this, transactionBuilder);
        return amount;
    }

    public void chargeMoney(String accountNumber, double amount) throws TransactionException {
        Transaction.TransactionBuilder transactionBuilder = new Transaction.TransactionBuilder().setRecipientNumber(accountNumber).setAmount(amount);
        Manager.MakeTransaction(this, transactionBuilder);
    }

    public void cancelTransaction(Transaction transaction) throws MyException {
        Manager.cancelTransaction(this, transaction);
    }

    @Override
    public boolean equals(Object o) {
        if (this == o)
            return true;
        if (o == null)
            return false;
        if (getClass() != o.getClass())
            return false;
        Client otherClient = (Client) o;
        return firstName.equals(otherClient.firstName) && secondName.equals(otherClient.secondName) &&
                address.equals(otherClient.address) && passportNumber.equals(otherClient.passportNumber);
    }

    public static class ClientBuilder {
        private String firstName = null;
        private String secondName = null;
        private String address = null;
        private String passportNumber = null;

        public ClientBuilder(String firstName, String secondName) {
            this.firstName = firstName;
            this.secondName = secondName;
        }

        public ClientBuilder setAddress(String address) {
            this.address = address;
            return this;
        }

        public ClientBuilder setPassportNumber(String passportNumber) {
            this.passportNumber = passportNumber;
            return this;
        }

        public Client getResult() {
            return new Client(firstName, secondName, address, passportNumber);
        }

        public ClientBuilder reset(String firstName, String secondName){
            this.firstName = firstName;
            this.secondName = secondName;
            address = null;
            passportNumber = null;
            return this;
        }
    }
}
