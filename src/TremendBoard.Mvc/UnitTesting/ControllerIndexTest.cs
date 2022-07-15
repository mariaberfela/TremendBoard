using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;
using TremendBoard.Mvc.Controllers;
using TremendBoard.Mvc.Models.UserViewModels;
using TremendBord.Mvc;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Controllers;
using TremendBord.Mvc;
using TremendBord.FunctionalTests;
using Hangfire;

namespace UnitTesting
{
    public class ControllerIndexTest
    {
        private CustomWebApplicationFactory<WebMarker> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void SetUp()
        {
            _factory = CustomWebApplicationFactory<WebMarker>.Instance();
            _client = _factory.CreateClient();
        }
        [Test]
        public void HomeController_Index_ReturnsAViewResult()
        {
            var dateService = new Mock<IDateTime>();
            dateService.Setup(service => service.Now)
               .Returns(DateTime.UtcNow);

            var controller = new HomeController(dateService.Object);

            // Act
            var result = controller.Index();

            // Assert
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.IsInstanceOf<ViewResult>(result);
        }

        // Testul asta e luat din proiect TremendBoard.FunctionalTests si am incercat sa il reproduc si pentru JobTestController dar nu prea mi-a iesit
        [Test]
        public void JobTestController_Index_ReturnsAViewResult()
        {
            var jobTestService = new Mock<IJobTestService>();
            jobTestService.Setup(service => service.FireAndForgetJob());
            jobTestService.Setup(service => service.ReccuringJob());
            jobTestService.Setup(service => service.DelayedJob());
            jobTestService.Setup(service => service.ContinuationJob());
            var backgroundJobClient = new Mock<IBackgroundJobClient>();
            var recurringJobManager = new Mock<IRecurringJobManager>();


            var controller = new JobTestController((IJobTestService)jobTestService, (IBackgroundJobClient)backgroundJobClient, (IRecurringJobManager)recurringJobManager);

            // Act
            var result = controller.CreateFireAndForgetJob();

            // Assert
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.IsInstanceOf<ViewResult>(result);
        }
    }
}
