using ItTakesAVillage.Models;
using System.Text.RegularExpressions;

namespace ItTakesAVillage.Contracts
{
    public interface INotificationService
    {
        Task NotifyGroup(DinnerInvitation dinnerInvitation);
        Task<int> Count(string userId);
        Task<List<Notification>> GetAllByUserId(string userId);
        Task UpdateIsRead(int notificationId);
    }
}
