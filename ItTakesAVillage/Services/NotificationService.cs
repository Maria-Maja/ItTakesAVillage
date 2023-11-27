using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
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
                   IsRead = false
               });

            await _context.SaveChangesAsync();
        }

    }
}
