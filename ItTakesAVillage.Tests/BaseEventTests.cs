using ItTakesAVillage.Contracts;
using ItTakesAVillage.Models;
using ItTakesAVillage.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItTakesAVillage.Tests
{
    public class BaseEventTests
    {
        private readonly Mock<IRepository<DinnerInvitation>> _dinnerInvitationRepositoryMock;
        private readonly DinnerInvitationService _sut;

        public BaseEventTests()
        {
            _dinnerInvitationRepositoryMock = new Mock<IRepository<DinnerInvitation>>();

            _sut = new DinnerInvitationService(_dinnerInvitationRepositoryMock.Object);
        }
        [Fact]
        public async Task Create_InvitationInFuture_ShouldReturnTrue()
        {
            // Arrange
            var futureDate = DateTime.Now.AddDays(1);
            var dinnerInvitation = new DinnerInvitation { DateTime = futureDate };

            _dinnerInvitationRepositoryMock.Setup(x => x.AddAsync(It.IsAny<DinnerInvitation>()))
                                          .Returns(Task.CompletedTask);

            // Act
            var actual = await _sut.Create(dinnerInvitation);

            // Assert
            Assert.True(actual);
            _dinnerInvitationRepositoryMock.Verify(x => x.AddAsync(It.IsAny<DinnerInvitation>()), Times.Once);
        }

        [Fact]
        public async Task Create_InvitationInPast_ShouldReturnFalse()
        {
            // Arrange
            var pastDate = DateTime.Now.AddDays(-1);
            var dinnerInvitation = new DinnerInvitation { DateTime = pastDate };

            // Act
            var actual = await _sut.Create(dinnerInvitation);

            // Assert
            Assert.False(actual);
            _dinnerInvitationRepositoryMock.Verify(x => x.AddAsync(It.IsAny<DinnerInvitation>()), Times.Never);
        }
    }
}
