using BusinessLogic.Implementations;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BusinessLogicTests
{
    public class RoleServiceTests
    {
        private readonly Mock<IDbRepository> repositoryStub = new();

        [Fact]
        public void GetAll_ReturnsAllData()
        {
            //Arrange
            var testDbItems = CreateTestItems();
            var roleService = new RoleService(repositoryStub.Object);
            repositoryStub.Setup(repo => repo.GetAll<Role>()).Returns(testDbItems);
            //Act
            var result = roleService.GetAll();
            //Assert
            Assert.Equal(testDbItems, result);
        }
        private static IQueryable<Role> CreateTestItems()
        {
            var testItems = new List<Role>
            {
                new Role
                {
                    Id = 1,
                    RoleName = "user"
                },
                new Role
                {
                    Id = 2,
                    RoleName = "admin"
                },
            };
            return testItems.AsQueryable();
        }
    }
}
