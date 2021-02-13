package com.company.BanksPackage;

import com.company.BanksPackage.myexceptions.*;

import java.time.*;
import java.time.temporal.ChronoUnit;
import java.util.*;

public class Deposit extends BankAccount{
    private double interestOnBalance;
    private final List<Double> interestOnEachDay = new ArrayList<>();
    private LocalDate closingDate;
    private final Timer rememberBalanceTimer;
    private final RememberBalanceTask rememberBalanceTask;
    private final Timer chargeInterestTimer;
    private final ChargeInterestTask chargeInterestTask;
    private final Timer closingTimer;
    private final ClosingTask closingTask;
    private LocalDateTime chargeInterestOnBalanceDate;

    Deposit(int bankId, String accountNumber, Client accountHolder, double balance, double interestOnBalance, LocalDate closingDate){
        super(bankId, accountNumber,accountHolder, balance);
        this.interestOnBalance = interestOnBalance;
        this.closingDate = closingDate == null ? LocalDate.now().plusMonths(12) : closingDate;
        rememberBalanceTimer = new Timer();
        rememberBalanceTask = new Deposit.RememberBalanceTask();
        rememberBalanceTimer.schedule(rememberBalanceTask, 0, 86400000);
        chargeInterestTimer = new Timer();
        chargeInterestTask = new Deposit.ChargeInterestTask();
        LocalDate now = LocalDate.now();
        LocalDate afterMonth = now.plusMonths(1);
        Date date = new Date();
        chargeInterestTimer.schedule(chargeInterestTask, date.getTime() + (ChronoUnit.DAYS.between(now, afterMonth) * 24 - 1) * 60 * 60 * 1000);
        closingTimer = new Timer();
        closingTask = new ClosingTask();
        closingTimer.schedule(closingTask, java.sql.Date.valueOf(closingDate));
        chargeInterestOnBalanceDate = LocalDateTime.now().plusMonths(1);
    }

    @Override
    AccountType GetType() {
        return AccountType.Deposit;
    }

    @Override
    void Charge(Transaction transaction){
        balance += transaction.getAmount();
        completedTransactions.add(transaction);
    }

    @Override
    double WithdrawalAmount(double amount) throws TransactionException {
        if (closingDate.compareTo(LocalDate.now()) < 0)
            throw new DepositNotClosedException();
        return amount;
    }

    @Override
    boolean checkOnCancelTransaction(Transaction transaction){
        if (LocalDateTime.now().isAfter(closingDate.atStartOfDay()))
            return true;
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

    class RememberBalanceTask extends TimerTask{
        @Override
        public void run(){
            interestOnEachDay.add(GetBalance() * interestOnBalance / 36500.);
        }
    }

    class ChargeInterestTask extends TimerTask {
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

    class ClosingTask extends TimerTask{
        @Override
        public void run(){
            rememberBalanceTimer.cancel();
            chargeInterestTimer.cancel();
        }
    }
}
