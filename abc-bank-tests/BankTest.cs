﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.CHECKING));
            
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());

            john.OpenAccount(new Account(Account.MAXI_SAVINGS));

            var banksummarytwoaccts = bank.CustomerSummary();
            var totalnumberaccts = bank.TotalNumberAccounts();

            // test value
            Assert.AreEqual(2, totalnumberaccts);

            // test with text
            Assert.AreEqual("Customer Summary\n - John (2 accounts)", banksummarytwoaccts);

            Customer customerBill = new Customer("Bill Smith");
            customerBill.OpenAccount(new Account(Account.MAXI_SAVINGS));
            bank.AddCustomer(customerBill);

            Assert.AreEqual(3, bank.TotalNumberAccounts());
            
        }

        [TestMethod]
        public void CheckingAccount() 
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);

            checkingAccount.Deposit(5289.98);

            var totalinterest = bank.totalInterestPaid();
            Assert.AreEqual(5.38998, totalinterest , DOUBLE_DELTA);


        }

        [TestMethod]
        public void Savings_account() 
        {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

            savingsAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() 
        {
            Bank bank = new Bank();
            Account maxiAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiAccount));

            maxiAccount.Deposit(3000.0);

            Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
