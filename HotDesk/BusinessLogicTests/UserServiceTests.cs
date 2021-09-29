using BusinessLogic.Implementations;
using DataAccessLayer;
using DataAccessLayer.Entities;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogicTests
{
    public class UserServiceTests
    {
        private readonly Mock<IDbRepository> repositoryStub = new();

        [Fact]
        public void GetAll_ReturnsAllData()
        {
            //Arrange
            var testDbItems = CreateTestItems();
            var userService = new UserService(repositoryStub.Object);
            repositoryStub.Setup(repo => repo.GetAll<User>()).Returns(testDbItems);
            //Act
            var result = userService.GetAll();
            //Assert
            Assert.Equal(testDbItems, result);
        }
        [Theory]
        [InlineData("admin")]
        [InlineData("TestUser")]
        public async Task GetByLoginAsync_ExistingLogin_ReturnsCorrectUser(string userLogin)
        {
            //Arrange
            var testDbItems = CreateTestItems().BuildMock();
            var userService = new UserService(repositoryStub.Object);
            repositoryStub.Setup(repo => repo.GetAll<User>()).Returns(testDbItems.Object);
            //Act
            var result = await userService.GetByLoginAsync(userLogin);
            //Assert
            Assert.Equal(userLogin, result.Login);
        }
        [Theory]
        [InlineData("NonExistingLogin")]
        [InlineData("User1337")]
        public async Task GetByLoginAsync_NonExistingLogin_ReturnsNull(string userLogin)
        {
            //Arrange
            var testDbItems = CreateTestItems().BuildMock();
            var userService = new UserService(repositoryStub.Object);
            repositoryStub.Setup(repo => repo.GetAll<User>()).Returns(testDbItems.Object);
            //Act
            var result = await userService.GetByLoginAsync(userLogin);
            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task CreateAsync_DefaultUser_CreatesUser()
        {
            //Arrange
            var testUser = new User()
            {
                Id = 3,
                Login = "TestUser",
                Password = "123456",
                RoleId = 2
            };
            var userService = new UserService(repositoryStub.Object);
            repositoryStub.Setup(repo => repo.Add(It.IsAny<User>())).Returns(Task.FromResult(testUser.Id));
            //Act
            var result = await userService.CreateAsync(testUser);
            //Assert
            Assert.Equal(testUser.Id, result);
        }
        private static IQueryable<User> CreateTestItems()
        {
            var testItems = new List<User>
            {
                new User
                {
                    Id = 1,
                    Login = "admin",
                    Password = "admin",
                    RoleId = 1
                },
                new User
                {
                    Id = 2,
                    Login = "Nezorin",
                    Password = "qwerty",
                    RoleId = 2
                },
                new User
                {
                    Id = 3,
                    Login = "TestUser",
                    Password = "123456",
                    RoleId = 2
                },
            };
            return testItems.AsQueryable();
        }
    }
}
