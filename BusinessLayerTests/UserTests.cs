using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BusinessLayer.Entities;

namespace BusinessLayerTests
{
    public class UserTests
    {
        [Fact]
        public void ValidationTest()
        {
            // Arrange
            User validUser = new User("Ilia", "Poeta", "em@c.com");
            User invalidNameUser = new User("", "Poeta", "em@c.com");
            User invalidSurnameUser = new User("Ilia", "", "em@c.com");

            // Act
            bool isValidValid = validUser.Validate();
            bool isInvalidNameValid = invalidNameUser.Validate();
            bool isInvalidSurnameValid = invalidSurnameUser.Validate();

            // Assert
            Assert.True(isValidValid);
            Assert.False(isInvalidNameValid);
            Assert.False(isInvalidSurnameValid);
        }

        [Fact]
        public void FullNameTest()
        {
            // Arrange
            User validUser = new User("Ilia", "Poeta", "em@c.com");
            User invalidNameUser = new User("", "Poeta", "em@c.com");
            User invalidSurnameUser = new User("Ilia", "", "em@c.com");
            string expectedValidFullName = "Ilia Poeta";
            string expectedInvalidName = "Poeta";
            string expectedInvalidSurname = "Ilia";

            // Act
            string validFullName = validUser.FullName;
            string invalidNameUserFullName = invalidNameUser.FullName;
            string invalidSurnameUserFullName = invalidSurnameUser.FullName;

            // Assert
            Assert.Equal(expectedValidFullName, validFullName);
            Assert.Equal(expectedInvalidName, invalidNameUserFullName);
            Assert.Equal(expectedInvalidSurname, invalidSurnameUserFullName);
        }

        [Fact]
        public void SharedWalletTest()
        {
            // Arrange
            User firstUser = new User("U1", "U1", "em@c.com");
            User secondUser = new User("U2", "U2", "em@c.com");

            Wallet wallet = new Wallet("Wallet", "Descr", "USD", 50);
            wallet.Categories.Add(null);

            wallet.AddTransaction(new Transaction(0m, "t0", "Description",
                "USD", null, DateTime.Now));
            firstUser.Wallets.Add(wallet);
            secondUser.Wallets.Add(wallet);

            firstUser.Wallets.Find(x => x.Name == wallet.Name).AddTransaction(new Transaction(10m, "t1", "Description",
                "USD", null, DateTime.Now));
            secondUser.Wallets.Find(x => x.Name == wallet.Name).AddTransaction(new Transaction(-100m, "t2", "Description",
                "USD", null, DateTime.Now));

            decimal expectedBalance = -40;

            // Act
            decimal balanceFirstUser = firstUser.Wallets.Find(x => x.Name == wallet.Name).Balance;
            decimal balanceSecondUser = secondUser.Wallets.Find(x => x.Name == wallet.Name).Balance;

            // Assert
            Assert.Equal(expectedBalance, balanceFirstUser);
            Assert.Equal(expectedBalance, balanceSecondUser);
        }
    }
}
