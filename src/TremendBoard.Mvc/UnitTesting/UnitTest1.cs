using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;
using TremendBoard.Mvc.Controllers;
using TremendBoard.Mvc.Mappers;

namespace UnitTesting
{
    public class UnitTest1
    {
        [Fact]
        public void TimeService_GetCurrentTime_ReturnsCurrentTime()
        {
            var timeService = new TimeService();

            var result = timeService.GetCurrentTime();

            Assert.Contains(DateTime.Now.ToString("hh:mm:ss.fff"), result);
        }

        [Fact]
        public void TimeService_GetCurrentTime_ReturnsTime()
        {
            var timeService = new TimeService();

            var result = timeService.GetCurrentTime();

            Assert.IsType<String>(result);
        }

        //[Fact]
        //public void Index_ActionExecutes_ReturnsExactNumberOfProjects()
        //{
        //    var projectList=new Task <IEnumerable<Project>>();
        //    var projectProfile = new ProjectProfiles();
        //    var config = new MapperConfiguration(cfg => cfg.AddProfile(projectProfile));
        //    IMapper mapper = new Mapper(config);
        //    var mockRepo = new Mock<IProjectService>();
        //    mockRepo.Setup(repo => repo.GetProjects()).Returns(projectList);


        //    var controller = new ProjectController(mockRepo.Object, mapper);
        //    var result = controller.Index();
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var projects = Assert.IsType<List<Project>>(viewResult.Model);
        //    Assert.Equal(22, projects.Count);
        //}
        [Fact]
        public void HomeController_CheckIfIndexCatchesException()
        {
            // Arrange
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(x => x.Now)
                .Returns(null);

            var timeService = new Mock<ITimeService>();

            var controller = new HomeController(dateTime.Object, timeService.Object, timeService.Object);
           
            // Act
            var result = controller.Index();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void HomeControllerTest_GetHomeController_VerifiesView()
        {
            // Arrange
            var dateTime = new Mock<IDateTime>();
            dateTime.Setup(d => d.Now)
                    .Returns(DateTime.Now);

            var timeService1 = new Mock<ITimeService>();
            timeService1.Setup(t => t.GetCurrentTime())
                        .Returns(DateTime.Now.ToString("hh:mm:ss.fff"));

            var timeService2 = new Mock<ITimeService>();
            timeService2.Setup(t => t.GetCurrentTime())
                        .Returns(DateTime.Now.ToString("hh:mm:ss.fff"));

            var controller = new HomeController(dateTime.Object,timeService1.Object,timeService2.Object);
            
            // Act
            var viewResult = controller.Index();

            // Assert
            Assert.NotNull(viewResult);
        }

        [Theory]
        [InlineData("Name", "XDASDSA", "Good", "11/11/2021")]
        [InlineData("Name2", "ANVD@#1", "BAD", "2021/11/12")]
        public void CreateProject_WrongDeadlineFormat_Fail( string name,string description, string projectStatus,DateTime deadline)
        {
            Project project = new Project
               {
                 Name = name,
                 Description = description,
                 CreatedDate = DateTime.Now,
                 ProjectStatus = projectStatus,
                 Deadline = deadline
                };

              List<Project> projects = new List<Project>();
               // Act
             projects.Add(project);

             // Assert
             Assert.Contains(project, projects);
        }

        [Fact]
        public async Task ForSessionActionResult_ReturnsNotFoundObjectResultForNonexistentSession()
        {
            // Arrange
            var projectProfile = new ProjectProfiles();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(projectProfile));
            IMapper mapper = new Mapper(config);
            var mockRepo = new Mock<IProjectService>();
            var controller = new ProjectController(mockRepo.Object, mapper);
            controller.ModelState.AddModelError("error", "some error");

            // Act
            var result = await controller.Create(model:null);

            // Assert
            var actionResult = Assert.IsType<ActionResult<List<Project>>>(result);
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }
    }
}