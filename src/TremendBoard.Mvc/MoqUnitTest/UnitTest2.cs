using Moq;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;

namespace MoqUnitTest
{
    public class UnitTest2
    {
        [Theory]
        [InlineData("2022-07-15 17:00:00.00001", "2022-07-15 17:00:00.00002")]
        public void TimeMoq_ReturnsCurrentTime_ReturnsTrue(DateTime time1, DateTime time2)
        {
            //1
            var timeMoq1 = new Mock<ITimeMoq>();
            var timeMoq2 = new Mock<ITimeMoq>();

            //2
            timeMoq1.Setup(x => x.GetTime()).Returns(time1);
            timeMoq2.Setup(x => x.GetTime()).Returns(time2);

            //3
            Assert.NotEqual(timeMoq1, timeMoq2);
        }
    }
}