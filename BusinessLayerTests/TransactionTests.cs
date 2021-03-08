using System;
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
            Transaction validTransaction = new Transaction(0.0m, "valid Transaction", "This is a valid Transaction", "USD",
                null, DateTime.Now);
            Transaction invalidNameTransaction = new Transaction(0.0m, "", "This is not a valid Transaction", 
                "USD", null, DateTime.Now);
            Transaction invalidDescrTransaction = new Transaction(0.0m, "invalid Transaction", "", 
                "USD", null, DateTime.Now);
            Transaction invalidCurrencyTransaction = new Transaction(0.0m, "invalid Transaction", "This is not a valid Transaction", 
                "UsD", null, DateTime.Now);

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
            Transaction transaction1 = new Transaction(0.0m, "Transaction1", "Description",
                "USD", null, new DateTime(2020, 1, 1));
            
            Transaction transaction2 = new Transaction(0.0m, "Transaction1", "Description",
                "USD", null, new DateTime(2020, 1, 1));

            Transaction transaction3 = new Transaction(0.0m, "Transaction", "Description",
                "USD", null, new DateTime(2020, 1, 2));

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
