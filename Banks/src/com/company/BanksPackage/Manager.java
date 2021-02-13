package com.company.BanksPackage;
import com.company.BanksPackage.myexceptions.*;

import java.time.*;
import java.util.*;

public class Manager {
    private static int bankCommonId = 0;
    private static int transactionCommonId = 0;
    private static Map<Integer, Bank> existingBanks = new HashMap<>();

    public static Bank CreateBank(Bank.BankBuilder bankBuilder) throws BankAlreadyExistsException {
        bankBuilder.setId(bankCommonId + 1);
        Bank bank = bankBuilder.GetResult();
        if (existingBanks.containsKey(bank.getName())){
            throw new BankAlreadyExistsException();
        }
        bankCommonId++;
        existingBanks.put(bankCommonId, bank);
        return bank;
    }

    static int getTransactionCommonId(){ return transactionCommonId; }

    static void incTransactionCommonId(){ transactionCommonId++; }

    static Transaction MakeTransaction(Client sender, Transaction.TransactionBuilder transactionBuilder) throws TransactionException{
        try {
            transactionBuilder.setDate(LocalDateTime.now(ZoneId.of("UTC+3"))).setId(++transactionCommonId);
            Transaction transaction = transactionBuilder.getResult();
            if (transaction.getAmount() <= 0)
                throw new InvalidTransactionAmountException();
            if (transaction.getSenderNumber() != null) {
                withdrawMoney(sender, transaction);
            }
            if (transaction.getRecipientNumber() != null){
                chargeMoney(transaction);
            }
            return transaction;
        }
        catch (MyException ex){
            transactionCommonId--;
            throw ex;
        }
    }

    static double withdrawMoney(Client sender, Transaction transaction) throws TransactionException{
        String accountNumber = transaction.getSenderNumber();
        double amount = transaction.getAmount();
        Bank bank = GetBank(accountNumber);
        Integer accountId = GetAccountId(accountNumber);
        CheckBankAndAccount(bank, accountId, accountNumber);
        if (!bank.verifyOwnership(sender, accountId))
            throw new NotYourAccountException(sender, accountNumber);
        if (bank.checkClientTrusted(accountId) || amount < bank.getTransactionAmountLimit()){
            bank.withdrawMoney(accountId, transaction);
            return amount;
        }
        throw new UntrustedClientException(accountNumber, bank.getTransactionAmountLimit());
    }

    static void chargeMoney(Transaction transaction) throws TransactionException{
        String accountNumber = transaction.getRecipientNumber();
        Bank bank = GetBank(accountNumber);
        Integer accountId = GetAccountId(accountNumber);
        if (bank == null)
            throw new TransactionNonexistentBankException(accountNumber);
        if (!bank.checkAccountExistence(accountId))
            throw new TransactionNonexistentAccountException(accountNumber);
        bank.chargeMoney(accountId, transaction);
    }

    static void cancelTransaction(Client client, Transaction transaction) throws MyException{
        Bank recipientBank = GetBank(transaction.getRecipientNumber());
        int recipientAccountId = GetAccountId(transaction.getRecipientNumber());
        if (recipientBank.getClient(recipientAccountId) != client)
            throw new CancelTransactionException(client);
        Bank senderBank = GetBank(transaction.getSenderNumber());
        int senderAccountId = GetAccountId(transaction.getSenderNumber());
        if (senderBank.checkAccountForCancelTransaction(senderAccountId, transaction) && recipientBank.checkAccountForCancelTransaction(recipientAccountId, transaction)) {
            senderBank.cancelTransaction(senderAccountId, transaction);
            recipientBank.cancelTransaction(recipientAccountId, transaction);
            return;
        }
        throw new AccountCannotCancelTransactionException(transaction.getSenderNumber(), transaction.getRecipientNumber());
    }

    private static Bank GetBank(String accountNumber){
        Integer bankId = Integer.parseInt(accountNumber.substring(0, 4));
        return existingBanks.get(bankId);
    }

    private static Integer GetAccountId(String accountNumber){
        return Integer.parseInt(accountNumber.substring(4, 8));
    }

    private static void CheckBankAndAccount(Bank bank, Integer accountId, String accountNumber) throws TransactionException{
        if (bank == null)
            throw new TransactionNonexistentBankException(accountNumber);
        if (!bank.checkAccountExistence(accountId))
            throw new TransactionNonexistentAccountException(accountNumber);
    }
}
