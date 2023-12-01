using ItTakesAVillage.Contracts;
using ItTakesAVillage.Models;
using ItTakesAVillage.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ItTakesAVillage.Tests
{
    public class NotificationTests
    {
        private readonly Mock<IGroupService> _groupServiceMock;
        private readonly Mock<IRepository<ItTakesAVillageUser>> _userRepositoryMock;
        private readonly Mock<IRepository<Notification>> _notificationRepositoryMock;
        private readonly NotificationService _sut;

        public NotificationTests()
        {
            _groupServiceMock = new Mock<IGroupService>();
            _userRepositoryMock = new Mock<IRepository<ItTakesAVillageUser>>();
            _notificationRepositoryMock = new Mock<IRepository<Notification>>();

            _sut = new NotificationService(_groupServiceMock.Object,
                                   _userRepositoryMock.Object,
                                   _notificationRepositoryMock.Object);
        }
        [Fact]
        public async Task CountAsync_NotificationsExist_ShouldReturnCorrectCount()
        {
            // Arrange
            var userId = "testUserId";
            var unreadNotifications = new List<Notification>
            {
                new Notification { UserId = userId, IsRead = false },
                new Notification { UserId = userId, IsRead = false },
            };

            _notificationRepositoryMock.Setup(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                                      .ReturnsAsync(unreadNotifications);

            // Act
            var actual = await _sut.CountAsync(userId);

            // Assert
            Assert.Equal(unreadNotifications.Count, actual);
            _notificationRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Notification, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task CountAsync_NoNotifications_ShouldReturnZero()
        {
            // Arrange
            var userId = "testUserId";
            var emptyList = new List<Notification>();

            _notificationRepositoryMock.Setup(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Notification, bool>>>()))
                                      .ReturnsAsync(emptyList);

            // Act
            var actual = await _sut.CountAsync(userId);

            // Assert
            Assert.Equal(0, actual);
            _notificationRepositoryMock.Verify(x => x.GetByFilterAsync(It.IsAny<Expression<Func<Notification, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_CreatorExists_ShouldAddNotificationWithCreatorName()
        {
            // Arrange
            var dinnerInvitation = new DinnerInvitation { CreatorId = "creatorId" };
            var userId = "testUserId";
            var creator = new ItTakesAVillageUser { Id = dinnerInvitation.CreatorId, FirstName = "John", LastName = "Doe" };

            _userRepositoryMock.Setup(x => x.GetAsync(dinnerInvitation.CreatorId))
                             .ReturnsAsync(creator);

            _notificationRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Notification>()))
                                      .Returns(Task.CompletedTask);

            // Act
            await _sut.CreateAsync(dinnerInvitation, userId);

            // Assert
            _notificationRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Notification>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_CreatorDoesNotExist_ShouldAddNotificationWithUnknownCreator()
        {
            // Arrange
            var dinnerInvitation = new DinnerInvitation { CreatorId = "nonExistentId" };
            var userId = "testUserId";

            _userRepositoryMock.Setup(x => x.GetAsync(dinnerInvitation.CreatorId))
                             .ReturnsAsync(null as ItTakesAVillageUser); 

            _notificationRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Notification>()))
                                      .Returns(Task.CompletedTask);
            // Act
            await _sut.CreateAsync(dinnerInvitation, userId);

            // Assert
            _notificationRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Notification>()), Times.Once);
        }

        [Fact]
        public async Task UpdateIsReadAsync_NotificationExists_ShouldUpdateAndCallUpdateAsync()
        {
            // Arrange
            var notificationId = 1;
            var existingNotification = new Notification { Id = notificationId, IsRead = false };

            _notificationRepositoryMock.Setup(x => x.GetAsync(notificationId))
                                      .ReturnsAsync(existingNotification);

            // Act
            await _sut.UpdateIsReadAsync(notificationId);

            // Assert
            Assert.True(existingNotification.IsRead);
            _notificationRepositoryMock.Verify(x => x.UpdateAsync(existingNotification), Times.Once);
        }

        [Fact]
        public async Task UpdateIsReadAsync_NotificationDoesNotExist_ShouldNotCallUpdateAsync()
        {
            // Arrange
            var nonExistentNotificationId = 999;

            _notificationRepositoryMock.Setup(x => x.GetAsync(nonExistentNotificationId))
                                      .ReturnsAsync(null as Notification);

            // Act
            await _sut.UpdateIsReadAsync(nonExistentNotificationId);

            // Assert
            _notificationRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Notification>()), Times.Never);
        }
    }
}
