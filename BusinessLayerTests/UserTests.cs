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
            User validUser = new User() { Name = "Ilia", Surname = "Poeta" };
            User invalidNameUser = new User() { Name = "", Surname = "Poeta" };
            User invalidSurnameUser = new User() { Name = "Ilia", Surname = "" };

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
            User validUser = new User() { Name = "Ilia", Surname = "Poeta" };
            User invalidNameUser = new User() { Name = "", Surname = "Poeta" };
            User invalidSurnameUser = new User() { Name = "Ilia", Surname = "" };
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
            User firstUser = new User();
            User secondUser = new User();

            Wallet wallet = new Wallet(50) { Name = "Wallet" };
            wallet.AddTransaction(new Transaction() { Name = "t0" });
            firstUser.AddWallet(wallet);
            secondUser.AddWallet(wallet);

            firstUser.GetWallet(wallet.Name).AddTransaction(new Transaction() { Name="t1", Sum=10 });
            secondUser.GetWallet(wallet.Name).AddTransaction(new Transaction() { Name="t2", Sum=-100 });

            double expectedBalance = -40;

            // Act
            double balanceFirstUser = firstUser.GetWallet(wallet.Name).Balance;
            double balanceSecondUser = secondUser.GetWallet(wallet.Name).Balance;

            // Assert
            Assert.Equal(expectedBalance, balanceFirstUser);
            Assert.Equal(expectedBalance, balanceSecondUser);
        }
    }
}
