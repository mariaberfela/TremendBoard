using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;
using Assert = Xunit.Assert;

namespace TimeServiceMoqTests
{
    [TestClass]
    public class UnitTest1
    {
        [Fact]
        public void TimeService_GetCurrentTime_ReturnsCorrectCurrentTime()
        {
            var ts = new Mock<ITimeService>();

            ts.Setup(x => x.GetCurrentTime())
                .Returns(DateTime.Now.ToString());

            var _time = new TimeService().GetCurrentTime();
            Assert.Equal(DateTime.Now.ToString("hh:mm:ss.fff tt"), _time.ToString());
        }

        [Theory]
        [InlineData("10:50:01.000 AM", "10:50:02.000 AM")]
        public void TimeService_GetCurrentTime_TimesAreEqual(string t1, string t2)
        {
            var ts = new Mock<ITimeService>();
            var tss = new Mock<ITimeService>();

            ts.Setup(x => x.GetCurrentTime()).Returns(t1);
            tss.Setup(x => x.GetCurrentTime()).Returns(t2);

            Assert.NotEqual(ts, tss);
        }
    }
}