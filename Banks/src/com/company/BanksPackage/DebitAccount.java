package com.company.BanksPackage;

import com.company.BanksPackage.myexceptions.*;

import java.time.*;
import java.time.temporal.ChronoUnit;
import java.util.*;

public class DebitAccount extends BankAccount{
    private final double interestOnBalance;
    private List<Double> interestOnEachDay = new ArrayList<>();
    private final Timer rememberBalanceTimer;
    private final RememberBalanceTask rememberBalanceTask;
    private final Timer chargeInterestTimer;
    private final ChargeInterestTask chargeInterestTask;
    private LocalDateTime chargeInterestOnBalanceDate;

    DebitAccount(int bankId, String accountNumber, Client accountHolder, double balance, double interestOnBalance){
        super(bankId, accountNumber, accountHolder, balance);
        this.interestOnBalance = interestOnBalance;
        rememberBalanceTimer = new Timer();
        rememberBalanceTask = new RememberBalanceTask();
        rememberBalanceTimer.schedule(rememberBalanceTask, 0, 86400000);
        chargeInterestTimer = new Timer();
        chargeInterestTask = new ChargeInterestTask();
        LocalDate now = LocalDate.now();
        LocalDate afterMonth = now.plusMonths(1);
        Date date = new Date();
        chargeInterestTimer.schedule(chargeInterestTask, date.getTime() + (ChronoUnit.DAYS.between(now, afterMonth) * 24 - 1) * 60 * 60 * 1000);
        chargeInterestOnBalanceDate = LocalDateTime.now().plusMonths(1);
    }

    @Override
    AccountType GetType() { return AccountType.Debit; }

    @Override
    double WithdrawalAmount(double amount){
        return amount;
    }

    class RememberBalanceTask extends TimerTask{
        @Override
        public void run(){
            interestOnEachDay.add(GetBalance() * interestOnBalance / 36500.);
        }
    }

    @Override
    boolean checkOnCancelTransaction(Transaction transaction){
        return chargeInterestOnBalanceDate.isAfter(transaction.getDate());
    }

    @Override
    void cancelTransaction(Transaction transaction) throws MyException{
        super.cancelTransaction(transaction);
        long day = ChronoUnit.DAYS.between(transaction.getDate(), LocalDateTime.now());
        if (getAccountNumber().equals(transaction.getSenderNumber())){
            for (long i = interestOnEachDay.size() - day - 1; i < interestOnEachDay.size(); i++){
                double newSum = interestOnEachDay.get((int)i) + transaction.getAmount() * interestOnBalance / 36500.;
                interestOnEachDay.set((int)i, newSum);
            }
        }
        else{
            for (long i = interestOnEachDay.size() - day - 1; i < interestOnEachDay.size(); i++){
                double newSum = interestOnEachDay.get((int)i) - transaction.getAmount() * interestOnBalance / 36500.;
                interestOnEachDay.set((int)i, newSum);
            }
        }
    }

    class ChargeInterestTask extends TimerTask{
        @Override
        public void run(){
            double sum = 0;
            for (double amount : interestOnEachDay) {
                sum += amount;
            }
            String bankNumber = String.valueOf(getBankId());
            while (bankNumber.length() < 4){
                bankNumber = "0" + bankNumber;
            }
            Transaction transaction = new Transaction.TransactionBuilder().setSenderNumber(bankNumber).setRecipientNumber(getAccountNumber()).
                    setAmount(sum).setDate(LocalDateTime.now(ZoneId.of("UTC+3"))).setId(Manager.getTransactionCommonId()).getResult();
            Manager.incTransactionCommonId();
            Charge(transaction);
            interestOnEachDay.clear();
            LocalDate now = LocalDate.now();
            LocalDate afterMonth = now.plusMonths(1);
            Date date = new Date();
            chargeInterestTimer.schedule(chargeInterestTask, date.getTime() + ChronoUnit.DAYS.between(now, afterMonth) * 24 * 60 * 60 * 1000);
            chargeInterestOnBalanceDate = LocalDateTime.now().plusMonths(1);
        }
    }
}
