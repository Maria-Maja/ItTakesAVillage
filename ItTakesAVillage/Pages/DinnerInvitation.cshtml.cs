using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using ItTakesAVillage.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ItTakesAVillage.Pages
{
    public class DinnerInvitationModel : PageModel
    {
        private readonly UserManager<ItTakesAVillageUser> _userManager;
        private readonly IDinnerInvitationService _dinnerInvitationService;
        private readonly INotificationService _notificationService;
        private readonly IGroupRepository _groupRepository;

        [BindProperty]
        public DinnerInvitation NewInvitation { get; set; } = new DinnerInvitation();
        public ItTakesAVillageUser? CurrentUser { get; set; }

        public DinnerInvitationModel(IDinnerInvitationService dinnerInvitationService, 
            UserManager<ItTakesAVillageUser> userManager,
            INotificationService notificationService,
            IGroupRepository groupRepository)
        {
            _dinnerInvitationService = dinnerInvitationService;
            _userManager = userManager;
            _notificationService = notificationService;
            _groupRepository = groupRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["GroupId"] = new SelectList(await _groupRepository.GetAllGroupsAsync(), "Id", "Name");
            CurrentUser = await _userManager.GetUserAsync(User);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                CurrentUser = await _userManager.GetUserAsync(User);
                if (CurrentUser != null)
                {
                    NewInvitation.CreatorId = CurrentUser.Id;
                    await _dinnerInvitationService.Create(NewInvitation);
                    //TODO Om ovan lyckas, ska en notification skickas till alla i gruppen
                    await _notificationService.NotifyGroup(NewInvitation);
                }
            }
            return RedirectToPage("/DinnerInvitation");
        }
    }
}
