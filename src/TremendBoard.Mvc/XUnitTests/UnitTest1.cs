using Microsoft.AspNetCore.Mvc;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Mvc.Controllers;
using TremendBoard.Mvc.Models.ProjectViewModels;

namespace XUnitTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("Unit Tests", "Trying out the unit tests", "2022-7-15", "Doing")]
        // TestMethod naming convention
        // UnitOfWork_ExpectedBehavior_ScenarioUnderTest
        public void Create_NewProject_ReturnsView(string name, string description, DateTime deadline, string projectStatus)
        {
            //Arrange
            ProjectDetailViewModel projectUT = new()
            {
                Name = name,
                Description = description,
                Deadline = deadline,
                ProjectStatus = projectStatus
            };
            var controller = new ProjectController(null);
            //Act
            Task<IActionResult> result = controller.Create(projectUT);
            //Assert
            Assert.NotNull(result);
        }
    }
}