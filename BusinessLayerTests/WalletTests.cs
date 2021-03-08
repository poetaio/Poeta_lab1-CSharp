using System;
using System.Drawing;
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
            Wallet validWallet = new Wallet("valid Wallet", "This is a valid wallet", "USD", 0m);
            Wallet invalidNameWallet = new Wallet("", "This is a valid wallet", "USD", 0m);
            Wallet invalidDescrWallet = new Wallet("valid Wallet", "", "USD", 0m);
            Wallet invalidCurrencyWallet = new Wallet("valid Wallet", "This is a valid wallet", "UsD", 0m);

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
            Wallet wallet = new Wallet("Ilia's Poeta", "This is my wallet", "UAH", 10000);
            wallet.Categories.Add(null);

            wallet.AddTransaction(new Transaction(14.5m, null, null, null, null, 
                DateTime.Now));
            wallet.AddTransaction(new Transaction(150m, null, null, null, null, 
                DateTime.Now.AddDays(-10)));
            wallet.AddTransaction(new Transaction(1100m, null, null, null, null, 
                DateTime.Now.AddDays(-50)));
            wallet.AddTransaction(new Transaction(-1000m, null, null, null, null, 
                DateTime.Now));

            decimal expectedBalance = 10264.5m;

            // Act
            decimal balance = wallet.Balance;

            // Assert
            Assert.Equal(expectedBalance, balance);
        }

        [Fact]
        public void LastMonthTransactionsTest()
        {
            // Arrange
            Wallet wallet = new Wallet("Ilia's Poeta", "This is my wallet", "UAH", 0m);
            wallet.Categories.Add(null);

            wallet.AddTransaction(new Transaction(14.5m, null, null, null, null, 
                DateTime.Now));
            wallet.AddTransaction(new Transaction(150m, null, null, null, null, 
                DateTime.Now.AddDays(-14)));
            wallet.AddTransaction(new Transaction(-15.5m, null, null, null, null, 
                DateTime.Now.AddDays(-1)));
            wallet.AddTransaction(new Transaction(30m, null, null, null, null, 
                DateTime.Now.AddDays(-10)));
            wallet.AddTransaction(new Transaction(-14.5m, null, null, null, null, 
                DateTime.Now.AddDays(-20)));
            // not included
            wallet.AddTransaction(new Transaction(-1000m, null, null, null, null, 
                DateTime.Now.AddDays(-32)));
            wallet.AddTransaction(new Transaction(5000m, null, null, null, null, 
                DateTime.Now.AddDays(-50)));

            decimal expectedIncome = 194.5m;
            decimal expectedExpenses = 30m;

            // Act
            decimal income = wallet.GetLastMonthIncome();
            decimal expenses = wallet.GetLastMonthExpenses();

            // Assert
            Assert.Equal(expectedIncome, income);
            Assert.Equal(expectedExpenses, expenses);
        }

        [Fact]
        public void ChangeTransactionTest()
        {
            // Arrange
            Wallet wallet = new Wallet("Ilia's Poeta", "This is my wallet", "UAH", 0m);

            wallet.Categories.Add(new Category("Old", "OLD", Color.White, null));
            wallet.Categories.Add(new Category("New", "New", Color.White, null));

            wallet.AddTransaction(new Transaction(14.5m, "old name", "old description",
                "OLD", new Category("Old", "OLD", Color.White, null), DateTime.Now));
            decimal expectedSum = 0;
            string expectedName = "new name";
            string expectedDescription = "new description";
            string expectedCurrency = "NEW";
            Category expectedCategory = new Category("New", "New", Color.White, null);
            DateTime expectedDate = DateTime.Now.AddDays(10);

            // Act
            wallet.setTransactionSum(0, expectedSum);
            wallet.setTransactionName(0, expectedName);
            wallet.setTransactionDescription(0, expectedDescription);
            wallet.setTransactionCurrency(0, expectedCurrency);
            wallet.setTransactionCategory(0, expectedCategory);
            wallet.setTransactionDate(0, expectedDate);

            // Assert
            Assert.Equal(expectedSum, wallet.getTransactionSum(0));
            Assert.Equal(expectedName, wallet.getTransactionName(0));
            Assert.Equal(expectedDescription, wallet.getTransactionDescription(0));
            Assert.Equal(expectedCurrency, wallet.getTransactionCurrency(0));
            Assert.Equal(expectedCategory, wallet.getTransactionCategory(0));
            Assert.Equal(expectedDate, wallet.getTransactionDate(0));

        }

        [Fact]
        public void RemoveTransactionTest()
        {
            // Arrange
            Wallet wallet = new Wallet("Ilia's Poeta", "This is my wallet", "UAH", 0m);
            wallet.Categories.Add(new Category("Old", "OLD", Color.White, null));

            wallet.AddTransaction(new Transaction(14.5m, "old name", "old description",
                "OLD", new Category("Old", "OLD", Color.White, null), DateTime.Now));

            // Act
            wallet.RemoveTransaction("old name");

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => wallet.setTransactionName(0,
                "New name"));
        }
    }

}
