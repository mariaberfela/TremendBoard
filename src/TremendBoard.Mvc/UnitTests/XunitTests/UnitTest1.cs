namespace ProjectServiceTests.XunitTests;

using TremendBoard.Infrastructure.Data.Models;
using Xunit;
public class UnitTest1
{
    [Theory]
    [InlineData("Name", "Description", "In Progress", "01/01/2022")]
    [InlineData("Name2", "Description2", "In Progress", "2022/01/02")]
    [InlineData("Name3", "Description3", "In Progress", "wrong format")]
    public void CreateProject_WrongDeadlineFormat_Fail(
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

        List<Project> projects = new List<Project>();
        // Act
        projects.Add(project);

        // Assert
        Assert.Contains(project, projects);

    }
}