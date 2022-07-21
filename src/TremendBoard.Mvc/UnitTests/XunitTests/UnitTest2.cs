using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Services;

namespace ProjectServiceTests.XunitTests
{
    public class UnitTest2
    {
        [Fact]
        public void TimeServiceDayOfWeek_returns_current_time_day_of_week()
        {

            TimeService _timeService = new TimeService();

            // Act
            var returnedTime = _timeService.DayOfWeek;
            var expectedTime = DateTime.Now.DayOfWeek.ToString();
            // Assert
            Assert.Equal(returnedTime, expectedTime);

        }
    }
}
