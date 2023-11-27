using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ItTakesAVillage.Pages
{
    public class DinnerInvitationModel : PageModel
    {
        private readonly ItTakesAVillageContext _context;
        private readonly UserManager<ItTakesAVillageUser> _userManager;
        private readonly IDinnerInvitationService _dinnerInvitationService;
        private readonly INotificationService _notificationService;

        [BindProperty]
        public DinnerInvitation NewInvitation { get; set; } = new DinnerInvitation();

        public ItTakesAVillageUser? CurrentUser { get; set; }

        public DinnerInvitationModel(ItTakesAVillageContext context, 
            IDinnerInvitationService dinnerInvitationService, 
            UserManager<ItTakesAVillageUser> userManager,
            INotificationService notificationService)
        {
            _context = context;
            _dinnerInvitationService = dinnerInvitationService;
            _userManager = userManager;
            _notificationService = notificationService;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
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
                    NewInvitation.UserId = CurrentUser.Id;
                    await _dinnerInvitationService.CreateDinnerInvitation(NewInvitation);
                    //TODO Om ovan lyckas, ska en notification skickas till alla i gruppen
                    await _notificationService.SendDinnerNotificationToGroup(NewInvitation);
                }
            }
            return RedirectToPage("/DinnerInvitation");
        }
    }
}
