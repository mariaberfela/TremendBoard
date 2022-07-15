using TremendBoard.Infrastructure.Services.Services;

namespace TremendBoardXunit
{
    public class UnitTest1
    {
        [Fact]
        // TestMethod naming convention
        // UnitOfWork_ExpectedBehavior_ScenarioUnderTest
        public void SystemDateTime_ReturnsCurrentDateTime_EqualToCurrentDateTime()
        {
            //Arrange
            string serviceDateTime;
            SystemDateTime systemDateTimeService = new SystemDateTime(); 

            //Act
            serviceDateTime = systemDateTimeService.Now.ToString();

            //Assert
            string currentDateTime = DateTime.Now.ToString();
            Xunit.Assert.Equal(serviceDateTime, currentDateTime);
        }
    }
}