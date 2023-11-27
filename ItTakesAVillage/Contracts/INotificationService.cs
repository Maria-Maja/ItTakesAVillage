using ItTakesAVillage.Models;
using System.Text.RegularExpressions;

namespace ItTakesAVillage.Contracts
{
    public interface INotificationService
    {
        Task SendDinnerNotificationToGroup(DinnerInvitation dinnerInvitation);
    }
}
