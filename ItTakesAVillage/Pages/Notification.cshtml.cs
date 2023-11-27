using ItTakesAVillage.Contracts;
using ItTakesAVillage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ItTakesAVillage.Pages
{
    public class NotificationModel : PageModel
    {
        private readonly UserManager<ItTakesAVillageUser> _userManager;
        private readonly INotificationService _notificationService;
      

        public ItTakesAVillageUser? CurrentUser { get; set; }
        public List<Notification> Notifications { get; set; } = new List<Notification>();
        public DinnerInvitation DinnerInvitation { get; set; }
        public NotificationModel(UserManager<ItTakesAVillageUser> userManager, INotificationService notificationService)
        {
            _userManager = userManager;
            _notificationService = notificationService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser != null)
                Notifications = await _notificationService.GetAllNotificationsByUserId(CurrentUser.Id);

            return Page();
        }
    }
}
