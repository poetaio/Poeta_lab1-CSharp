using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BusinessLayer.Entities;

namespace BusinessLayerTests
{
    public class TransactionTests
    {
        [Fact]
        public void ValidationTest()
        {

            // Arrange
            Transaction validTransaction = new Transaction()
            {
                Name = "valid Transaction",
                Description = "This is a valid Transaction",
                Currency = "USD"
            };
            Transaction invalidNameTransaction = new Transaction()
            {
                Name = "",
                Description = "This is not a valid Transaction",
                Currency = "USD"
            };
            Transaction invalidDescrTransaction = new Transaction()
            {
                Name = "invalid Transaction",
                Description = "",
                Currency = "USD"
            };
            Transaction invalidCurrencyTransaction = new Transaction()
            {
                Name = "invalid transaction",
                Description = "This is not a valid Transaction",
                Currency = "UsD"
            };

            // Act
            bool isNameValid = invalidNameTransaction.Validate();
            bool isDescrValid = invalidDescrTransaction.Validate();
            bool isCurrencyValid = invalidCurrencyTransaction.Validate();
            bool isValidOneValid = validTransaction.Validate();

            // Assert
            Assert.False(isNameValid);
            Assert.False(isDescrValid);
            Assert.False(isCurrencyValid);
            Assert.True(isValidOneValid);
        }

        [Fact]
        public void CompareToTest()
        {
            // Arrange
            Transaction transaction1 = new Transaction()
            {
                Date = new DateTime(2020, 1, 1),
                Name = "t"
            };

            Transaction transaction2 = new Transaction()
            {
                Date = new DateTime(2020, 1, 1),
                Name = "t"
            };

            Transaction transaction3 = new Transaction()
            {
                Date = new DateTime(2020, 1, 2),
                Name = "t3"
            };

            // Act
            bool compare12 = transaction1.CompareTo(transaction2) == 0;
            bool compare23 = transaction2.CompareTo(transaction3) == -1;
            bool compare32 = transaction3.CompareTo(transaction2) == 1;

            // Assert
            Assert.True(compare12);
            Assert.True(compare23);
            Assert.True(compare32);
        }
    }
}
