using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace ItTakesAVillage.Pages
{
    public class GroupModel : PageModel
    {
        [BindProperty]
        public Group? NewGroup { get; set; }
        public void OnGet()
        {

        }
    }
}
