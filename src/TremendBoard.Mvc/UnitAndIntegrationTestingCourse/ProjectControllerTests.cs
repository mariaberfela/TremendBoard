using Microsoft.AspNetCore.Mvc;
using TremendBoard.Mvc.Controllers;

namespace UnitAndIntegrationTestingCourse
{
    public class ProjectControllerTests
    {
        [Fact]
        public void Create_NewProject_ReturnsView()
        {
            //Arrange
            var controller = new ProjectController(null);

            //Act
            var result = controller.Create() as ViewResult;

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void EditProject_WithInvalidID_ShouldThrowException()
        {
            //Arrange
            var controller = new ProjectController(null);

            //Act & Assert
            Assert.Throws<AggregateException>(() => controller.Edit("1").Result);

        }

        //This should fail because the method throws AggregateException, not a NullReferenceException
        [Fact]
        public void EditProject_WithInvalidID_ShouldThrowExceptionTest2()
        {
            //Arrange
            var controller = new ProjectController(null);

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => controller.Edit("1").Result);

        }

        [Fact]
        public void EditProject_WithValidID_ShouldThrowException()
        {
            //Arrange
            var controller = new ProjectController(null);

            //Act & Assert
            Assert.Throws<AggregateException>(() => controller.Edit("736659b7-f2e3-4c26-a1b1-2f9e6eb7a44d").Result);

        }
    }
}