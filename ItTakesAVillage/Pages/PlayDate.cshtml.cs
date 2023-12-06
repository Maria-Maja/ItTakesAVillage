using ItTakesAVillage.Contracts;
using ItTakesAVillage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ItTakesAVillage.Pages
{
    public class PlayDateModel : PageModel
    {
        private readonly UserManager<ItTakesAVillageUser> _userManager;
        private readonly IGroupService _groupService;
        private readonly INotificationService _notificationService;

        public ItTakesAVillageUser? CurrentUser { get; set; }
        public PlayDate NewPlayDate { get; set; } = new PlayDate();
        public List<Notification> Notifications { get; set; }
        public List<Models.Group?> GroupsOfCurrentUser { get; set; } = new List<Group?>();
        public PlayDateModel(UserManager<ItTakesAVillageUser> userManager,
            IGroupService groupService,
            INotificationService notificationService)
        {
            _userManager = userManager;
            _groupService = groupService;
            _notificationService = notificationService;
        }
        public async Task<IActionResult> OnGet()
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            if (CurrentUser != null)
            {
                ViewData["GroupId"] = new SelectList(await _groupService.GetGroupsByUserId(CurrentUser.Id), "Id", "Name");
                GroupsOfCurrentUser = await _groupService.GetGroupsByUserId(CurrentUser.Id);
                ViewData["GroupId"] = new SelectList(GroupsOfCurrentUser, "Id", "Name");
                Notifications = await _notificationService.GetAsync(CurrentUser.Id);
            }
            return Page();
        }
    }
}
