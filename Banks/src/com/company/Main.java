package com.company;
import com.company.BanksPackage.*;
import com.company.BanksPackage.myexceptions.*;
import java.util.*;

public class Main {
    public static void main(String[] args){
        try{
            Bank.BankBuilder bankBuilder = new Bank.BankBuilder().SetName("Alfa").SetTransactionAmountLimit(10).SetCreditAccountConditions(50).SetDebitAccountConditions(3);
            Bank alfa = Manager.CreateBank(bankBuilder);
            System.out.println(alfa.getName());
            DebitAccountBuilder debit = alfa.getDebitAccountBuilder();
            debit.SetStartBalance(1000);
            Client client = new Client.ClientBuilder("Denis", "Andreev").setAddress("Vyazma").setPassportNumber("12345").getResult();
            Client otherClient = new Client.ClientBuilder("Deniss", "Andreev").setAddress("Vyazma").setPassportNumber("12345").getResult();
            String accountNumber = alfa.createAccount(client, debit);
            double sum = client.withdrawMoney(accountNumber, 500);
            String otherAccountNumber = alfa.createAccount(otherClient, debit);
            Transaction.TransactionBuilder transactionBuilder = new Transaction.TransactionBuilder().setAmount(250).setSenderNumber(accountNumber).setRecipientNumber(otherAccountNumber);
            Transaction transaction = client.MakeTransaction(transactionBuilder);
            otherClient.cancelTransaction(transaction);
            System.out.println("----");
        }
        catch(MyException ex){
            System.out.println(ex.getMessage());
            StackTraceElement[] trace = ex.getStackTrace();
            for(StackTraceElement element : trace){
                System.out.println(element.toString());
            }
            System.exit(1);
        }
    }
}
