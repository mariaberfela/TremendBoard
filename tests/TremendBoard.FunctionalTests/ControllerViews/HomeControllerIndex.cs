using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Mvc.Controllers;
using TremendBord.Mvc;

namespace TremendBord.FunctionalTests.ControllerViews
{
    public class HomeControllerIndex
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
        public void Index_ReturnsAViewResult()
        {
            //weird and probably wrong fix
            // Arrange
            var dateService = new Mock<IDateTime>();
            dateService.Setup(service => service.Now)
                .Returns(DateTime.UtcNow);

            var timeService = new Mock<ITimeService>();

            var logService = new Mock<ILogger<HomeController>>();

            var controller = new HomeController(dateService.Object, timeService.Object, timeService.Object, logService.Object);

            // Act
            var result = controller.Index();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task ReturnsViewWithCorrectMessage()
        {
            HttpResponseMessage response = await _client.GetAsync("/Home/Index");
            response.EnsureSuccessStatusCode();
            string stringResponse = await response.Content.ReadAsStringAsync();

            Assert.IsTrue(stringResponse.Contains("Project Management with Scrum"));
        }
    }
}