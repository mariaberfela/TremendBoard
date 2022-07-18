using TremendBoard.Infrastructure.Services.Services;

namespace TimeServiceTestsXUnit
{
    public class UnitTest1
    {
        [Fact]
        public void TimeService_GetTime_ReturnCorrectCurrentTime()
        {
            var ts = new TimeService().GetCurrentTime();
            var ts2 = new TimeService().GetCurrentTime();
            Assert.Equal(ts, ts2);
        }

        [Theory]
        [InlineData("Good morning!","Good morning!")]
        [InlineData("Have a great day!","Have a great day!")]
        public void TimeService_Greet_ReturnCorrectDateTime(string expected, string actual)
        {
            actual = new TimeService().Greet();
            expected = "Good morning!";

            Assert.Equal(expected,actual);
        }

        [Fact]
        public void TimeService_GetTime_ReturnNoOfDaysInMonth()
        {
            var expected = 30;
            var actual = DateTime.DaysInMonth(2022, 6);

            Assert.Equal(expected, actual);
        }

    }
}