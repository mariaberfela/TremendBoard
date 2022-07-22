using Castle.Core.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace ProjectServiceTests.MoqTests
{
    public class UnitTest5
    {
        [Theory]
        [InlineData("Name", "Description", "In Progress", "01/01/2022")]
        [InlineData("Name2", "Description2", "In Progress", "2022/01/02")]
        [InlineData("Name3", "Description3", "In Progress", "01/01/2022")]
        public void Get_Project_Name(
        string name,
        string description,
        string projectStatus,
        DateTime deadline)
        {

            Project project = new Project
            {
                Name = name,
                Description = description,
                CreatedDate = DateTime.Now,
                ProjectStatus = projectStatus,
                Deadline = deadline
            };
            var _mockProjectService = new Mock<IProjectService>();
            _mockProjectService
                .Setup(x => x.GetProjectName(project))
            .Returns(project.Name);
            // Act

            // Assert
            Assert.Equal(project.Name, _mockProjectService.Object.GetProjectName(project));

        }
    }
}
