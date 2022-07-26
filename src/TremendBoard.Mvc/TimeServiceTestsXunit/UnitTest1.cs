using TremendBoard.Infrastructure.Services.Services;
using Xunit;
using TheoryAttribute = NUnit.Framework.TheoryAttribute;

namespace TimeServiceTestsXunit;
public class UnitTest1
{
    [Fact]
    public void TimeService_GetTime(string time1, string time2)
    {
        time1 = new TimeService().GetCurrentTime();
        time2 = new TimeService().GetCurrentTime();
        Assert.Equals(time1, time2);
    }
    [Theory]
    [InlineData("Good Morning!", "Good Morning!")]
    [InlineData("Hello!", "Hello!")]
    public void TimeService_ReturnCorrectDateTime(string time1, string time2)
    {
        time1 = new TimeService().GoodDay();
        time2 = "Hello!";
        Assert.Equals(time1, time2);
    }
    [Theory]
    [InlineData("Hello!", 10)]
    public void DayService_CalculateRemainingHoursInThisDay(string time, int hours)
    {
        var dayService = new DayService(time);

        dayService.CalculateRemainingHoursInThisDay(hours);

        Assert.Equals(hours, dayService.CalculateRemainingHoursInThisDay(30));
    }
  

}