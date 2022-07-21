using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Serilog;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Concrete;
using TremendBoard.Infrastructure.Services.DTOs;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;
using TremendBoard.Mvc.Controllers;
using TremendBoard.Mvc.Models;
using TremendBoard.Mvc.Models.ProjectViewModels;
using TremendBord.FunctionalTests;
using TremendBord.Mvc;
using static TremendBoard.Mvc.Controllers.HomeController;

namespace TremendBoard.XUnitTests
{
    public class UnitTest1
    {
        
        private CustomWebApplicationFactory<WebMarker> _factory;
        private HttpClient _client;

        public UnitTest1()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Log.Information("UnitTest1: Accesed constructor.");
            _factory = CustomWebApplicationFactory<WebMarker>.Instance();
            _client = _factory.CreateClient();
        }


        [Fact]
        public void TestTimeService()
        {
            TimeService timeService = new();

            Log.Information("TestTimeService: Working...");

            Assert.True(timeService.GetCurrentTime().CompareTo(DateTime.Now.ToString()) <= 0);
        }

        [Fact]
        public void TestHomeControllerIndex()
        {
            // Arrange
            DateTime someTime = new(year: 2022, month: 7, day: 20, hour: 18, minute: 40, second: 35);
            DateTime otherTime = new(year: 2022, month: 7, day: 20, hour: 12, minute: 40, second: 35);

            var mockDateTime = new Mock<IDateTime>();
            mockDateTime
                .Setup(dateTime => dateTime.Now)
                .Returns(someTime);

            var mockTimeService1 = new Mock<ITimeService>();
            mockTimeService1
                .Setup(timeService => timeService.GetCurrentTime())
                .Returns(someTime.ToString());

            var mockTimeService2 = new Mock<ITimeService>();
            mockTimeService2
                .Setup(timeService => timeService.GetCurrentTime())
                .Returns(otherTime.ToString());

            var controller = new HomeController(mockDateTime.Object, mockTimeService1.Object, mockTimeService2.Object);

            // Act
            Log.Information("Entering Index method.");
            var result = controller.Index();
            Log.Information("Exited Index method.");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.ViewData);
            Assert.True(viewResult.ViewData.Any());
            Assert.NotNull(viewResult.ViewData["Message"]);
            Assert.True(
                (viewResult.ViewData["Message"])
                .Equals(DayTime.Evening.ToString()));
            Assert.NotNull(viewResult.ViewData["timeService1"]);
            Assert.NotNull(viewResult.ViewData["timeService2"]);
            Assert.False(
                viewResult.ViewData["timeService1"]
                .Equals(viewResult.ViewData["timeService2"]));
        }

        [Fact]
        public void TestHomeControllerError()
        {
            // Arrange
            var mockDateTime = new Mock<IDateTime>();
            mockDateTime
                .Setup(dateTime => dateTime.Now)
                .Returns(DateTime.Now);

            var mockTimeService1 = new Mock<ITimeService>();
            mockTimeService1
                .Setup(timeService => timeService.GetCurrentTime())
                .Returns(DateTime.Now.ToString());

            var mockTimeService2 = new Mock<ITimeService>();
            mockTimeService2
                .Setup(timeService => timeService.GetCurrentTime())
                .Returns(DateTime.Now.ToString());

            var controller = new HomeController(mockDateTime.Object, mockTimeService1.Object, mockTimeService2.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.TraceIdentifier = new string("Mocked TraceIdentifier :D");

            // Act
            Log.Information("Entering Error method.");
            var result = controller.Error();
            Log.Information("Exited Error method.");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
            Assert.NotNull(viewResult.ViewData);
            Assert.NotNull(viewResult.ViewData.Model);
            var model = Assert
                .IsAssignableFrom<ErrorViewModel>(
                    viewResult.ViewData.Model);
            Assert.True(model.ShowRequestId);
        }

        [Fact]
        public async Task TestProjectControllerAddUser()
        {

            // Arrange
            var mockUoW = new Mock<IUnitOfWork>();
            var mockProjectService = new Mock<IProjectService>();
            var mockMapper = new Mock<IMapper>();

            var controller = new ProjectController(mockUoW.Object, mockProjectService.Object, mockMapper.Object);
            controller.ModelState.AddModelError("UserId", "Required");
            var model = new ProjectUserDetailViewModel();

            // Act
            var result = await controller.AddUser(model);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Theory]
        [InlineData("SomeMockName", "RandomDescription", "JustMockStatus")]
        [InlineData("OtherMockName", "NoSpecialDescription", "BasicStatusPlaceholder")]
        public void TestProjectServiceUpdateProjectFields(string name, string description, string status)
        {
            //Arrange
            var mockUoW = new Mock<IUnitOfWork>();

            DateTime someTime = new(year: 2022, month: 7, day: 20, hour: 18, minute: 40, second: 35);
            DateTime otherTime = new(year: 2022, month: 7, day: 20, hour: 12, minute: 40, second: 35);

            Project ProjectFactory(int i) => new()
            {
                Id = i.ToString(),
                Name = name + i,
                Description = description + i,
                ProjectStatus = status + i,
                Deadline = someTime/*,
                CreatedDate = otherTime,
                CompletedDate = someTime*/
            };

            var dtoStr = "dto";
            ProjectDetailDTO ProjectDTOFactory(int i) => new()
            {
                Id = i.ToString(),
                Name = name + i + dtoStr,
                Description = description + i + dtoStr,
                ProjectStatus = status + i + dtoStr,
                Deadline = someTime/*,
                CreatedDate = otherTime,
                CompletedDate = someTime*/
            };

            var range = Enumerable.Range(1, 23);
            var projList = range.Select(i => ProjectFactory(i)).ToList();
            var projDTOList = range.Select(i => ProjectDTOFactory(i)).ToList();
            var pairs = projList.Zip(projDTOList);

            var service = new ProjectService(mockUoW.Object);

            // Act
            var pairsUpdated =
                pairs
                .Select(pair => service.UpdateProjectFields(pair.First, pair.Second))
                .Zip(
                    pairs
                    .Select(pair => pair.Second));

            bool compare(Project proj, ProjectDetailDTO projDTO)
            {
                return proj.Id.Equals(projDTO.Id) &&
                    proj.Name.Equals(projDTO.Name) &&
                    proj.Description.Equals(projDTO.Description) &&
                    proj.ProjectStatus.Equals(projDTO.ProjectStatus) &&
                    proj.Deadline.Equals(projDTO.Deadline);
            }

            Func<(Project, ProjectDetailDTO), bool> compareFunc = projTouple => compare(projTouple.Item1, projTouple.Item2);

            Func<bool, bool, bool> andAccumulator = (b1, b2) => b1 && b2;

            bool res =
                pairsUpdated
                .Aggregate(
                    true,
                    (acc, projTouple) => acc && compareFunc(projTouple));

            Assert.True(res);
        }

    }
}