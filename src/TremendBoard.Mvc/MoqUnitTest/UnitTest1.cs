using Moq;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;

namespace MoqUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void TimeMoq_ReturnsCurrentTime_ReturnsTrue()
        {
            //1
            var timeMoqService = new Mock<ITimeMoq>();
            //2
            timeMoqService.Setup(x => x.GetTime()).Returns(DateTime.Now);
            //3
            var result = new TimeMoqService().GetTime();
            Assert.Equal(DateTime.Now.Date.ToString(), 
                         result.Date.ToString());
        }
    }
}