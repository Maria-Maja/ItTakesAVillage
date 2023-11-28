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

        public async Task<List<Notification>> GetAllByUserId(string userId) =>  await _context.Notifications.Where(x => x.UserId == userId).ToListAsync();
        private async Task<Notification> GetOneById(int notificationId) => await _context.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId); //TODO nullchecka metod

        public async Task<int> Count(string userId)
        {
            var result = await GetAllByUserId(userId);
            return result.Where(x => x.IsRead == false).Count();
        }

        public async Task NotifyGroup(DinnerInvitation dinnerInvitation)
        {
            var groupMembers = await _groupService.GetMembers(dinnerInvitation.GroupId);
            if (!groupMembers.IsNullOrEmpty())
            {
                foreach (var member in groupMembers)
                {
                    await Create(dinnerInvitation, member.Id); //TODO nullchecka member.id
                }
            }
        }

        private async Task Create(DinnerInvitation dinnerInvitation, string userId)
        {
            var creator = await _userService.GetById(dinnerInvitation.CreatorId);
            _context.Notifications.Add
               (new Notification
               {
                   UserId = userId,
                   Title = $"Matlag hos {creator.FirstName} {creator.LastName}",
                   IsRead = false,
                   RelatedEvent = dinnerInvitation
               });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateIsRead(int notificationId)
        {
            var existingNotification = await GetOneById(notificationId); 
            
            if(existingNotification != null)
            {
                existingNotification.IsRead = true;

                await _context.SaveChangesAsync();
            }
        }

    }
}
