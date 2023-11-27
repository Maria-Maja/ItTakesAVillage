using ItTakesAVillage.Models;
using System.Text.RegularExpressions;

namespace ItTakesAVillage.Contracts
{
    public interface INotificationService
    {
        Task SendDinnerNotificationToGroup(DinnerInvitation dinnerInvitation);
        Task<int> GetAmountOfUnreadNotifications(string userId);
        Task<List<Notification>> GetAllNotificationsByUserId(string userId);
        Task UpdateNotificationIsRead(Notification notification);
    }
}
