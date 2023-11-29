using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using ItTakesAVillage.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using System.Reflection.Metadata;

namespace ItTakesAVillage.Tests
{
    public class GroupTests
    {

        public GroupTests()
        {

        }

        [Fact]
        public async Task CanAddUserToGroup()
        {
            //Arrange
            var userId = "userId123";
            var groupId = 1;

            var serviceMock = new Mock<IGroupService>();
            serviceMock
                .Setup(x =>x.AddUser(userId, groupId))
                .ReturnsAsync(true);

            var sut = new TestClasses.GroupTests(serviceMock.Object);

            //Act
            var actual = await sut.AddUser(userId, groupId);

            //Assert
            Assert.True(actual);
        }
    }
}