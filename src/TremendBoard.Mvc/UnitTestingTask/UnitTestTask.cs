using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;
using TremendBoard.Mvc.Controllers;

namespace UnitTestingTask
{
    public class UnitTestTask
    {
        [Fact]
        public void TimeService_GetCurrentTime_ReturnsString()
        {
            var timeService = new TimeService();

            var result = timeService.GetCurrentTime();

            Assert.IsType<String>(result);
        }

        [Fact]
        public void TimeService_GetCurrentTime_ReturnsCorrectTime()
        {
            var timeService = new TimeService();

            var result = timeService.GetCurrentTime();

            Assert.Contains(DateTime.Now.ToString("HH:mm:ss"), result);
        }

        [Fact]
        public void TimeService_GetCurrentTime_IsCurrentTimeAWorkingHour_ReturnsBool()
        {
            var timeService = new TimeService();

            var result = timeService.IsCurrentTimeAWorkingHour();

            Assert.IsType<bool>(result);
        }

        [Theory]
        [InlineData("fafgasgfd;hglkfdjhlkg")]
        [InlineData("")]
        [InlineData("fullltime")]
        public void WorkService_CalculateRemainingWeekHours_CheckIfThroesException(string rol)
        {
            var workService = new WorkService(rol);

            Assert.ThrowsAny<Exception>(() => workService.CalculateRemainingWeekHours(13));
        }

        [Theory]
        [InlineData("fulltime", 10)]
        public void WorkService_CalculateRemainingWeekHours_CheckIfItWorks(string rol, int ore)
        {
            var workService = new WorkService(rol);

            workService.CalculateRemainingWeekHours(ore);

            Assert.Equal(ore, workService.CalculateRemainingWeekHours(30));
        }
        [Fact]
        public void HomeController_CheckIfIndexCatchesException()
        {
            var dateService = new Mock<IDateTime>();
            dateService.Setup(service => service.Now)
                .Returns(null);

            var timeService = new Mock<ITimeService>();

            var logService = new Mock<ILogger<HomeController>>();

            var controller = new HomeController(dateService.Object, timeService.Object, timeService.Object, logService.Object);

            var result = controller.Index();

            Assert.NotNull(result);
        }

        [Fact]
        public void HomeController_CheckIfAboutWorks()
        {
            var dateService = new Mock<IDateTime>();
            dateService.Setup(service => service.Now)
                .Returns(DateTime.Now);

            var timeService = new Mock<ITimeService>();

            var logService = new Mock<ILogger<HomeController>>();

            var controller = new HomeController(dateService.Object, timeService.Object, timeService.Object, logService.Object);
            var data = DateTime.UtcNow;
            var result = controller.About(dateService.Object);

            Assert.IsType<ViewResult>(result);

        }


    }
}