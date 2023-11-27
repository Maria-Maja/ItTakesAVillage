using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace ItTakesAVillage.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IGroupService _groupService;
        private readonly ItTakesAVillageContext _context;
        private readonly IUserService _userService;

        public NotificationService(IGroupService groupService,
            ItTakesAVillageContext context,
            IUserService userService)
        {
            _groupService = groupService;
            _userService = userService;
            _context = context;
        }

        public async Task<List<Notification>> GetAllNotificationsByUserId(string userId) =>  await _context.Notifications.Where(x => x.UserId == userId).ToListAsync();
        private async Task<Notification> GetNotificationById(int notificationId) => await _context.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId);

        public async Task<int> GetAmountOfUnreadNotifications(string userId)
        {
            var result = await GetAllNotificationsByUserId(userId);
            return result.Where(x => x.IsRead == false).Count();
        }

        public async Task SendDinnerNotificationToGroup(DinnerInvitation dinnerInvitation)
        {
            var groupMembers = await _groupService.GetGroupMembersByGroupId(dinnerInvitation.GroupId);
            if (!groupMembers.IsNullOrEmpty())
            {
                foreach (var member in groupMembers)
                {
                    await CreateDinnerNotification(dinnerInvitation, member.Id);
                }
            }
        }

        private async Task CreateDinnerNotification(DinnerInvitation dinnerInvitation, string userId)
        {
            var creator = await _userService.GetUserById(dinnerInvitation.UserId);
            _context.Notifications.Add
               (new Notification
               {
                   UserId = userId,
                   GroupId = dinnerInvitation.GroupId,
                   DateTime = dinnerInvitation.DateTime,
                   Title = $"Matlag hos: {creator.FirstName} {creator.LastName}",
                   IsRead = false,
                   RelatedEvent = dinnerInvitation
               });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateNotificationIsRead(Notification notification)
        {
            var existingNotification = await GetNotificationById(notification.Id); 
            
            if(existingNotification != null)
            {
                existingNotification.UserId = notification.UserId;
                existingNotification.GroupId = notification.GroupId;
                existingNotification.DateTime = notification.DateTime;
                existingNotification.Title = notification.Title;
                existingNotification.IsRead = true;

                await _context.SaveChangesAsync();
            }
        }

    }
}
