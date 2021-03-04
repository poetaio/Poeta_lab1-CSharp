using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BusinessLayer.Entities;

namespace BusinessLayerTests
{
    public class WalletTests
    {

        [Fact]
        public void ValidationTest()
        {
            // Arrange
            Wallet validWallet = new Wallet(0.0) { 
                Name = "valid Wallet", 
                Description = "This is a valid wallet", 
                Currency = "USD"
            };
            Wallet invalidNameWallet = new Wallet(0.0)
            {
                Name = "",
                Description = "This is not a valid wallet",
                Currency = "USD"
            };
            Wallet invalidDescrWallet = new Wallet(0.0)
            {
                Name = "invalid Wallet",
                Description = "",
                Currency = "USD"
            };
            Wallet invalidCurrencyWallet = new Wallet(0.0)
            {
                Name = "invalid Wallet",
                Description = "This is not a valid wallet",
                Currency = "UsD"
            };

            // Act
            bool isNameValid = invalidNameWallet.Validate();
            bool isDescrValid = invalidDescrWallet.Validate();
            bool isCurrencyValid = invalidCurrencyWallet.Validate();
            bool isValidOneValid = validWallet.Validate();

            // Assert
            Assert.False(isNameValid);
            Assert.False(isDescrValid);
            Assert.False(isCurrencyValid);
            Assert.True(isValidOneValid);
        }

        [Fact]
        public void BalanceTest()
        {
            // Arrange
            Wallet wallet = new Wallet(10000)
            {
                Name = "Ilia's Poeta",
                Description = "This is my wallet",
                Currency = "UAH"
            };
            var transactions = new SortedSet<Transaction>()
            {
                new Transaction() {Sum = 14.5, Date = DateTime.Now },
                new Transaction() {Sum = 150, Date = DateTime.Now.AddDays(-10) },
                new Transaction() {Sum = 1100, Date = DateTime.Now.AddDays(-50) },
                new Transaction() {Sum = -1000 }
            };
            wallet.Transactions = transactions;
            double expectedBalance = 10264.5;

            // Act
            double balance = wallet.Balance;

            // Assert
            Assert.Equal(expectedBalance, balance);
        }

        [Fact]
        public void LastMonthTransactionsTest()
        {
            // Arrange
            Wallet wallet = new Wallet()
            {
                Name = "Ilia's Poeta",
                Description = "This is my wallet",
                Currency = "UAH"
            };
            var transactions = new SortedSet<Transaction>()
            {
                new Transaction() {Sum = 14.5, Date = DateTime.Now },
                new Transaction() {Sum = 150, Date = DateTime.Now.AddDays(-10) },
                new Transaction() {Sum = -15.5, Date = DateTime.Now.AddDays(-2) },
                new Transaction() {Sum = 30, Date = DateTime.Now.AddDays(-1) },
                new Transaction() {Sum = -14.5, Date = DateTime.Now.AddDays(-20) },
                // not included
                new Transaction() {Sum = -1000, Date = DateTime.Now.AddDays(-50) },
                new Transaction() {Sum = 1000, Date = DateTime.Now.AddDays(-50) }
            };
            wallet.Transactions = transactions;
            double expectedIncome = 194.5;
            double expectedExpenses = 30;

            // Act
            double income = wallet.GetLastMonthIncome();
            double expenses = wallet.GetLastMonthExpenses();

            // Assert
            Assert.Equal(expectedIncome, income);
            Assert.Equal(expectedExpenses, expenses);
        }
    }
}
