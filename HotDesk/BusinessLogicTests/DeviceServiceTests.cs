using BusinessLogic.Implementations;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogicTests
{
    public class DeviceServiceTests
    {
        private readonly Mock<IDbRepository> repositoryStub = new();

        [Fact]
        public void GetAll_ReturnsAllData()
        {
            //Arrange
            var testDbItems = CreateTestItems();
            var deviceService = new DeviceService(repositoryStub.Object);
            repositoryStub.Setup(repo => repo.GetAll<Device>()).Returns(testDbItems);
            //Act
            var result = deviceService.GetAll();
            //Assert
            Assert.Equal(testDbItems, result);
        }

        [Fact]
        public async Task CreateAsync_DefaultDevice_CreatesDevice()
        {
            //Arrange
            var testDevice = new Device()
            {
                Id = 1,
                DeviceName = "Mouse"
            };
            var deviceService = new DeviceService(repositoryStub.Object);
            repositoryStub.Setup(repo => repo.Add(It.IsAny<Device>())).Returns(Task.FromResult(testDevice.Id));
            //Act
            var result = await deviceService.CreateAsync(testDevice);
            //Assert
            Assert.Equal(testDevice.Id, result);
        }
        private static IQueryable<Device> CreateTestItems()
        {
            var testItems = new List<Device>
            {
                new Device
                {
                    Id = 1,
                    DeviceName = "Mouse"
                },
                new Device
                {
                    Id = 2,
                    DeviceName = "Keyboard"
                },
                new Device
                {
                    Id = 3,
                    DeviceName = "Monitor"
                },
                new Device
                {
                    Id = 4,
                    DeviceName = "Lamp"
                }
            };
            return testItems.AsQueryable();
        }
    }
}

