using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using ItTakesAVillage.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ItTakesAVillage.Tests
{
    public class GroupTests
    {

        public GroupTests()
        {

        }

        [Fact]
        public void CanAddUserToGroup()
        {
            //Arrange
            var userId = "userId123";
            var groupId = 1;

            var options = new DbContextOptionsBuilder<ItTakesAVillageContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            var contextMock = new Mock<ItTakesAVillageContext>(options);
            var userGroupsMock = new Mock<DbSet<UserGroup>>();
            contextMock.Setup(x => x.Set<UserGroup>()).Returns(userGroupsMock.Object);

            var sut = new GroupService(contextMock.Object);

            //Act
            sut.AddUser(userId, groupId);

            //Assert
            userGroupsMock.Verify(mock => mock.AddAsync(It.IsAny<UserGroup>(), default), Times.Once);
            contextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}