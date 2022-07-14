using TremendBoard.Infrastructure.Services.Services;

namespace xUnitTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void TimeService_ReturnsDateTime_EqualToCurrentDateTime()
        {
            // Arrange
            string returnedDateTime;
            TimeService timeService = new TimeService();

            // Act
            returnedDateTime = timeService.GetCurrentTime();

            // Assert
            string currentDateTime = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss.fff");
            Xunit.Assert.Equal(currentDateTime, returnedDateTime);
        }
    }
}