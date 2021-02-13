package com.company.BanksPackage;

import java.time.*;

public class Transaction {
    private final int id;
    private String senderNumber;
    private String recipientNumber;
    private double amount;
    private LocalDateTime date;

    int getId() { return id; }

    public String getSenderNumber(){ return senderNumber; }

    public String getRecipientNumber(){ return recipientNumber; }

    public double getAmount(){ return amount; }

    public LocalDateTime getDate(){ return date; }

    Transaction(int id, String senderNumber, String recipientNumber, double amount, LocalDateTime date){
        this.id = id;
        this.senderNumber = senderNumber;
        this.recipientNumber = recipientNumber;
        this.amount = amount;
        this.date = date;
    }

    public static class TransactionBuilder{
        private int id;
        private String senderNumber = null;
        private String recipientNumber = null;
        private double amount;
        private LocalDateTime date;

        TransactionBuilder setId(int id){
            this.id = id;
            return this;
        }

        public TransactionBuilder setSenderNumber(String senderNumber){
            this.senderNumber = senderNumber;
            return this;
        }

        public TransactionBuilder setRecipientNumber(String recipientNumber){
            this.recipientNumber = recipientNumber;
            return this;
        }

        public TransactionBuilder setAmount(double amount){
            this.amount = amount;
            return this;
        }

        TransactionBuilder setDate(LocalDateTime date){
            this.date = date;
            return this;
        }

        Transaction getResult(){
            return new Transaction(id, senderNumber, recipientNumber, amount, date);
        }
    }
}
