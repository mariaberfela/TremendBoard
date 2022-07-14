using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Concrete;
using TremendBoard.Infrastructure.Services.Services;

namespace xUnitTestProject
{
    public class UnitTest3
    {
        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(9, 5.2729536371528454168241257238955, 14.2729536371528454168241257238955)]
        [InlineData(1, double.MaxValue, double.MaxValue)]
        public void Addition_VariousDoubleValues_SuccessfulAddition(double a, double b, double expected)
        {
            // Arrange

            // Act
            double actual = a + b;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}