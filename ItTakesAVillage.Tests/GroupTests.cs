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
        //[Fact]
        //public async Task AddGroup_UserNotInGroupWithSameName_ShouldReturnGroupId()
        //{
        //    // Arrange
        //    string name = "Family";

        //    var userRepositoryMock = new Mock<IRepository<ItTakesAVillageUser>>();
        //    var groupRepositoryMock = new Mock<IRepository<Group>>();
        //    var userGroupRepositoryMock = new Mock<IRepository<UserGroup>>();

        //    var group = new Group { Name = name };
        //    var expectedId = 1;

        //    groupRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Group>()))
        //                      .Callback((Group g) => g.Id = expectedId);

        //    var sut = new GroupService(groupRepositoryMock.Object,
        //        userRepositoryMock.Object,
        //        userGroupRepositoryMock.Object);

        //    // Act
        //    var actual = await sut.Save(group);

        //    // Assert
        //    Assert.Equal(expectedId, actual);
        //    groupRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Group>()), Times.Once);
        //}


        [Fact]
        public async Task AddGroup_UserIsInGroupWithSameName_ShouldReturnZero() //begränsa om användaren redan är med i en grupp med samma namn?
        {
            //Arrange


            //Act

            //Assert
        }


        [Fact]
        public async Task AddUser_UserNotInList_ShouldAddUserAndReturnTrue()
        {
            // Arrange
            var userId = "expectedUserId";
            var groupId = 1;
            var userGroups = new List<UserGroup>();

            var groupRepositoryMock = new Mock<IRepository<Group>>();
            var userRepositoryMock = new Mock<IRepository<ItTakesAVillageUser>>();
            userRepositoryMock.Setup(x => x.GetAsync(userId)).ReturnsAsync(new ItTakesAVillageUser { Id = userId });

            var userGroupRepositoryMock = new Mock<IRepository<UserGroup>>();
            userGroupRepositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(userGroups);

            var sut = new GroupService(groupRepositoryMock.Object,
                userRepositoryMock.Object,
                userGroupRepositoryMock.Object);

            // Act
            var actual = await sut.AddUser(userId, groupId);

            // Assert
            Assert.True(actual);
            userGroupRepositoryMock.Verify(x => x.AddAsync(It.IsAny<UserGroup>()), Times.Once);
        }
        [Fact]
        public async Task AddUser_UserAlreadyInList_ShouldNotAddUserAndReturnFalse()
        {
            // Arrange
            var expectedUserId = "expectedUserId";
            var expectedGroupId = 1;
            var userGroups = new List<UserGroup>
            {
                new UserGroup { UserId = expectedUserId, GroupId = expectedGroupId }
            };

            var userRepositoryMock = new Mock<IRepository<ItTakesAVillageUser>>();
            userRepositoryMock.Setup(x => x.GetAsync(expectedUserId)).ReturnsAsync(new ItTakesAVillageUser { Id = expectedUserId });

            var userGroupRepositoryMock = new Mock<IRepository<UserGroup>>();
            userGroupRepositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(userGroups);
            var groupRepositoryMock = new Mock<IRepository<Group>>();

            var sut = new GroupService(groupRepositoryMock.Object,
                userRepositoryMock.Object,
                userGroupRepositoryMock.Object);

            // Act
            var actual = await sut.AddUser(expectedUserId, expectedGroupId);

            // Assert
            Assert.False(actual);
        }
    }
}