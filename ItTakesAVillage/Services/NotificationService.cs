using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using ItTakesAVillage.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace ItTakesAVillage.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IGroupService _groupService;
        private readonly IRepository<ItTakesAVillageUser> _userRepository;
        private readonly IRepository<Notification> _notificationRepository;

        public NotificationService(IGroupService groupService,
            IRepository<ItTakesAVillageUser> userRepository,
            IRepository<Notification> notificationRepository)
        {
            _groupService = groupService;
            _userRepository = userRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<List<Notification>> GetAsync(string userId) => await _notificationRepository.GetByFilterAsync(x => x.UserId == userId);
        public async Task<int> CountAsync(string userId)
        {
            var result = await _notificationRepository.GetByFilterAsync(x => x.UserId == userId && x.IsRead == false);

            return result.Count();
        }
        public async Task NotifyGroupAsync(DinnerInvitation dinnerInvitation)
        {
            var groupMembers = await _groupService.GetMembers(dinnerInvitation.GroupId);

            if (!groupMembers.IsNullOrEmpty())
            {
                foreach (var member in groupMembers)
                {
                    if (member != null)
                        await CreateAsync(dinnerInvitation, member.Id);
                }
            }
        }
        public async Task UpdateIsReadAsync(int notificationId)
        {
            var existingNotification = await _notificationRepository.GetAsync(notificationId);

            if (existingNotification != null)
            {
                existingNotification.IsRead = true;

                await _notificationRepository.UpdateAsync(existingNotification);
            }
        }
        private async Task CreateAsync(DinnerInvitation dinnerInvitation, string userId)
        {
            var creator = await _userRepository.GetAsync(dinnerInvitation.CreatorId);
            var newNotification = new Notification
            {
                UserId = userId,
                Title = creator == null ? $"Matlag hos okänd" : $"Matlag hos {creator.FirstName} {creator.LastName}",
                IsRead = false,
                RelatedEvent = dinnerInvitation
            };
            await _notificationRepository.AddAsync(newNotification);
        }
    }
}
