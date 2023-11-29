using ItTakesAVillage.Models;
using Microsoft.EntityFrameworkCore;

namespace ItTakesAVillage.Contracts
{
    public interface IItTakesAVillageContext : IDisposable
    {
        DbSet<Models.BaseEvent> Events { get; set; }
        DbSet<Models.Group> Groups { get; set; }
        DbSet<UserGroup> UserGroups { get; set; }
        DbSet<Notification> Notifications { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
    }
}
