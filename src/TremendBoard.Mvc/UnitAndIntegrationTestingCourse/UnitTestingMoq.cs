using Microsoft.AspNetCore.Mvc;
using Moq;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Controllers;

namespace UnitAndIntegrationTestingCourse
{
    public class UnitTestingMoq
    {
        [Fact]
        public void GetCurrentTime_ReturnsCurrentTime()
        {
            //1
            var timeServiceMoq = new Mock<ITimeService>();
            var dateTimeServiceMoq = new Mock<IDateTime>();

            //2
            timeServiceMoq
                .Setup(x => x.GetCurrentTime())
                .Returns("2022-07-14 17:50:22");
            dateTimeServiceMoq
                .Setup(x => x.Now)
                .Returns(new DateTime(2020, 10, 10));

            //3
            var homeController = new HomeController(dateTimeServiceMoq.Object, timeServiceMoq.Object, timeServiceMoq.Object);
            var result = homeController.Index() as ViewResult;

            //The ' - ' is added here because the ViewData["timeService1"] contains also the result of GetGUID method
            //In this case there is nothing returned from GetGUID
            Assert.Equal("2022-07-14 17:50:22 - ", result.ViewData["timeService1"]);
        }

        [Theory]
        [InlineData("2022-07-14 17:50:22.12312", "2022-07-14 17:50:22.12311")]
        [InlineData("2022-07-14 17:50:22.00000", "2022-07-14 17:50:22.00000")]
        public void GetCurrentTime_ServiceLifetimeIsTransient(string valueOfTimeService1, string valueOfTimeService2)
        {
            //1
            var timeServiceMoq = new Mock<ITimeService>();
            var timeServiceMoq2 = new Mock<ITimeService>();
            var dateTimeServiceMoq = new Mock<IDateTime>();

            //2
            timeServiceMoq
                .Setup(x => x.GetCurrentTime())
                .Returns(valueOfTimeService1);
            timeServiceMoq2
                .Setup(x => x.GetCurrentTime())
                .Returns(valueOfTimeService2);
            dateTimeServiceMoq
                .Setup(x => x.Now)
                .Returns(new DateTime(2020, 10, 10));

            //3
            var homeController = new HomeController(dateTimeServiceMoq.Object, timeServiceMoq.Object, timeServiceMoq2.Object);
            var result = homeController.Index() as ViewResult;

            Assert.NotEqual(result.ViewData["timeService1"], result.ViewData["timeService2"]);
        }
    }
}
