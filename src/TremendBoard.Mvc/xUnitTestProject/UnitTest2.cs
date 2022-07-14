using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Services;

namespace xUnitTestProject
{
    public class UnitTest2
    {
        [Theory]
        [InlineData("Project 1", "Description for \"Project 1.\"", "In Progress", "2022-10-2")]
        [InlineData("Project 2", "", "To Do", "2023-10-2")]
        [InlineData("", "", "", "")]
        public void Project_ExtendsList_AfterAppend(
            string name,
            string description,
            string projectStatus,
            DateTime deadline)
        {
            // Arrange
            Project project = new Project
            {
                Name = name,
                Description = description,
                ProjectStatus = projectStatus,
                Deadline = deadline
            };
            List<Project> projects = new List<Project>();

            // Act
            projects.Add(project);

            // Assert
            Xunit.Assert.True(projects.Count == 1);
            Xunit.Assert.Contains<Project>(project, projects);
        }
    }
}