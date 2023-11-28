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
        private readonly IDinnerInvitationService _dinnerInvitationService;

        public ItTakesAVillageUser? CurrentUser { get; set; }
        [BindProperty]
        public Notification? Notification { get; set; }
        public List<Notification> Notifications { get; set; } = new();
        public List<DinnerInvitation> DinnerInvitations { get; set; } = new();
        public NotificationModel(UserManager<ItTakesAVillageUser> userManager,
            INotificationService notificationService,
            IDinnerInvitationService dinnerInvitationService)
        {
            _userManager = userManager;
            _notificationService = notificationService;
            _dinnerInvitationService = dinnerInvitationService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            DinnerInvitations = await _dinnerInvitationService.GetAll();

            if (CurrentUser != null)
                Notifications = await _notificationService.GetAllByUserId(CurrentUser.Id);


            return Page();
        }
        public async Task<IActionResult> OnPostHandleAccordionClick()
        {
            if (Notification.Id != 0)
            {
                await _notificationService.UpdateIsRead(Notification.Id);
            }
            return RedirectToPage("/Notification");
        }
    }
}
