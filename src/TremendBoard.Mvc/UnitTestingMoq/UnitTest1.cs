using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;
using Xunit;
using Assert = NUnit.Framework.Assert;
using TheoryAttribute = Xunit.TheoryAttribute;

namespace UnitTestingMoq
{
    public class Tests
    {
        [TestClass]
        public class UnitTest1
        {
            [Fact]
            public void TimeService_GetCurrentTime_ReturnsCorrectCurrentTime()
            {
                var test1 = new Mock<ITimeService>();
                test1.Setup(x => x.GetCurrentTime())
                    .Returns(DateTime.Now.ToString());

                var time = new TimeService().GetCurrentTime();
                Assert.Equals(DateTime.Now.ToString("hh:mm:ss.fff"), time.ToString());
            }

            [Theory]
            [InlineData("11:00:01.000", "11:00:02.000")]
            public void TimeService_GetCurrentTime_TimesAreEqual(string time1, string time2)
            {
                var test1 = new Mock<ITimeService>();
                var test2 = new Mock<ITimeService>();

                test1.Setup(x => x.GetCurrentTime()).Returns(time1);
                test2.Setup(x => x.GetCurrentTime()).Returns(time2);

                Assert.That(test2, Is.Not.EqualTo(test1));
            }
        }
    }
}