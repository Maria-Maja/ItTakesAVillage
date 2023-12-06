using ItTakesAVillage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItTakesAVillage.Pages
{
    public class PlayDateModel : PageModel
    {
        private readonly UserManager<ItTakesAVillageUser> _userManager;
        public ItTakesAVillageUser? CurrentUser { get; set; }
        public PlayDateModel(UserManager<ItTakesAVillageUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            return Page();
        }
    }
}
