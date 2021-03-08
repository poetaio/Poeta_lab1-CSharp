using BusinessLayer.Entities;
using Xunit;
using System.Drawing;

namespace BusinessLayerTests
{
    public class CategoryTests
    {

        [Fact]
        public void ValidationTest()
        {
            // Arrange
            Category validCategory = new Category("Category", "This is a valid category", 
                Color.White, null);
            Category invalidNameCategory = new Category("", "This is a valid category",
                Color.White, null);
            Category invalidDescrCategory = new Category("Category", "",
                Color.White, null);

            // Act
            bool isNameValid = invalidNameCategory.Validate();
            bool isDescrValid = invalidDescrCategory.Validate();
            bool isValidOneValid = validCategory.Validate();

            // Assert
            Assert.True(isValidOneValid);
            Assert.False(isNameValid);
            Assert.False(isDescrValid);
        }
    }
}
