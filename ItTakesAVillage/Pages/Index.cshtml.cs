using ItTakesAVillage.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItTakesAVillage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ItTakesAVillageUser> _userManager;

        public ItTakesAVillageUser MyUser { get; set; }
        public IndexModel(UserManager<ItTakesAVillageUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGet()
        {
            MyUser = await _userManager.GetUserAsync(User);
            var id = MyUser.Id;
        }
    }
}
