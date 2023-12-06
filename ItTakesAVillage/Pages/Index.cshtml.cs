using ItTakesAVillage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItTakesAVillage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ItTakesAVillageUser> _userManager;
        public ItTakesAVillageUser? CurrentUser { get; set; }

        public IndexModel(UserManager<ItTakesAVillageUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            return Page();
        }
    }
}
