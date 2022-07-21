using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Controllers;

namespace ProjectServiceTests.MoqTests
{
    public class UnitTest4
    {
        [Fact]
        public void Get_Current_Time()
        {

            var _mockTimeService = new Mock<ITimeService>();

            _mockTimeService
                .Setup(x => x.GetCurrentTime)
            .Returns(DateTime.Now);


            // Act
            var returnedTime = _mockTimeService.Object.GetCurrentTime.ToString("yyyy-mm-dd hh:mm");
            var expectedTime = DateTime.Now.ToString("yyyy-mm-dd hh:mm");
            // Assert
            Assert.Equal(returnedTime, expectedTime);

        }
    }
}
