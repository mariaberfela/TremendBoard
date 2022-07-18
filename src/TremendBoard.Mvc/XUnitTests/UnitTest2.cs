using Microsoft.AspNetCore.Mvc;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Mvc.Controllers;
using TremendBoard.Mvc.Models.ProjectViewModels;

namespace XUnitTests
{
    public class UnitTest2
    {
        [Theory]
        [InlineData("", "Trying out the unit tests", "2022-10-10", "Doing")]
        [InlineData("Unit Tests", "", "2022-10-10", "Doing")]
        // TestMethod naming convention
        // UnitOfWork_ExpectedBehavior_ScenarioUnderTest
        public void Edit_ProjectWithInvalidFields_ThrowsException(string name, string description, DateTime deadline, string projectStatus)
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
            //Assert
            Assert.Throws<AggregateException>(() => controller.Edit(projectUT).Result);
        }
    }
}