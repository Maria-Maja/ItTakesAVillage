using ItTakesAVillage.Contracts;
using ItTakesAVillage.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ItTakesAVillage.TestClasses
{
    public class MockItTakesAVillageContext : Mock<IItTakesAVillageContext>
    {
        public MockItTakesAVillageContext()
        {
            // Konfigurera mocken efter behov
            Setup(x => x.Events).Returns(Mock.Of<DbSet<Models.BaseEvent>>());
            Setup(x => x.Groups).Returns(Mock.Of<DbSet<Models.Group>>());
            Setup(x => x.UserGroups).Returns(Mock.Of<DbSet<UserGroup>>());
            Setup(x => x.Notifications).Returns(Mock.Of<DbSet<Notification>>());

            // Konfigurera andra metoder och DbSet-egenskaper om det behövs
        }
    }
}
