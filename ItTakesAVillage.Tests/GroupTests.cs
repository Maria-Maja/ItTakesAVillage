using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using ItTakesAVillage.Services;
using ItTakesAVillage.TestClasses;
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
        public async Task AddUser_UserNotInGroup_ReturnsTrue()
        {
            // Arrange
            var userId = "someUserId";
            var groupId = 1;

            // Skapa en mock av DbContext
            var mockDbContext = new MockItTakesAVillageContext();

            // Använd mocken i din GroupService
            var groupService = new GroupService(mockDbContext.Object, new Mock<IUserService>().Object);

            // Act
            var result = await groupService.AddUser(userId, groupId);

            // Assert
            Assert.True(result);
            // Add additional assertions if needed
        }

        [Fact]
        public async Task CanAddUser()
        {
            // Arrange
            var optionsbuilder = new DbContextOptionsBuilder<ItTakesAVillageContext>();
            var contextmock = new Mock<ItTakesAVillageContext>(optionsbuilder.Options);
            var userservicemock = new Mock<IUserService>();

            var sut = new GroupService(contextmock.Object, userservicemock.Object);

            var userId = "someUserId";
            var groupId = 1;

            //Act
            var actual = await sut.AddUser(userId, groupId);

            //Assert
            Assert.True(actual);
            contextmock.Verify(x=> x.SaveChangesAsync(default), Times.Once);
        }




        [Fact]
        public async Task CanAddUserToGroup()
        {
            //Arrange
            var userId = "userId123";
            var groupId = 1;

            var serviceMock = new Mock<IGroupService>();
            serviceMock
                .Setup(x => x.AddUser(userId, groupId))
                .ReturnsAsync(true);

            var sut = new TestClasses.GroupTests(serviceMock.Object);

            //Act
            var actual = await sut.AddUser(userId, groupId);

            //Assert
            Assert.True(actual);
        }

        //[Fact]
        //public async Task ReturnsFalseIfUserIsAlreadyInGroup()
        //{
        //    // Arrange
        //    var userId = "existinguser123";
        //    var userId2 = "nonexistinguser";
        //    var groupId = 1;

        //    var userGroups = new List<UserGroup>
        //    {
        //        new UserGroup { UserId = userId, GroupId = groupId }
        //    };

        //    var groupServiceMock = new Mock<IGroupService>();
        //    groupServiceMock.Setup(x => x.GetAllUserGroups()).ReturnsAsync(userGroups);

        //    var sut = new TestClasses.GroupTests(groupServiceMock.Object);

        //    // Act
        //    var actual = await sut.AddUser("nonexisting", groupId);

        //    // Assert
        //    Assert.False(actual);
        //}
    }
}