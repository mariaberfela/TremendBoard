using Moq;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Concrete;
using TremendBoard.Infrastructure.Services.Interfaces;
using TremendBoard.Infrastructure.Services.Services;
using TremendBoard.Mvc.Models.UserViewModels;

namespace xUnitTestProject
{
    public class UserRepositoryTests
    {
        [Fact]
        public void Users_ReturnLastNamesByFirstName_ReturnLastNames()
        {
            // Arrange
            var userRepositoryStub = new Mock<IUserRepository>();

            // Act
            userRepositoryStub
                .Setup(x => x.GetUsersLastNameByFirstName("Alexandru"))
                .Returns(new List<string>
                {
                    "Sasu",
                    "Popescu",
                    "Tudor",
                    "Petru"
                });

            // Assert
            var userRepository = new UserRepository(userRepositoryStub.Object, null);
            IEnumerable<string> result = userRepository.GetUsersLastNameByFirstName("Alexandru");
            Assert.Equal(4, result.Count());
        }

        [Theory]
        [InlineData("Sasu")]
        [InlineData("")]
        [InlineData("Bogdan")]
        public void User_ReturnFirstNameByLastName_ReturnFirstName(string lastName)
        {
            // Arrange
            var userRepositoryStub = new Mock<IUserRepository>();

            // Act
            userRepositoryStub
                .Setup(x => x.GetUserFirstNameByLastName(lastName))
                .Returns((string firstName) => { return firstName; });

            // Assert
            var userRepository = new UserRepository(userRepositoryStub.Object, null);
            string result = userRepository.GetUserFirstNameByLastName(lastName);
            Assert.NotNull(result);
        }
    }
}