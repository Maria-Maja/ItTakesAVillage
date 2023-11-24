using ItTakesAVillage.Contracts;
using ItTakesAVillage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;

namespace ItTakesAVillage.Pages
{
    public class GroupModel : PageModel
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;
        private readonly UserManager<ItTakesAVillageUser> _userManager;

        [BindProperty]
        public ItTakesAVillageUser CurrentUser { get; set; }
        [BindProperty]
        public Models.Group NewGroup { get; set; } = new Models.Group();

        public GroupModel(IGroupService groupService, IUserService userService, UserManager<ItTakesAVillageUser> userManager)
        {
            _groupService = groupService;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<IActionResult> OnGet()
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            return Page();
        }

        public async Task<IActionResult> OnPostNewGroupAsync()
        {
            if (ModelState.IsValid)
            {
                var newGroupId = await _groupService.SaveGroup(NewGroup);
                var user = _userService.GetUserByUserName(User.Identity.Name);
                if (newGroupId != 0 && user.UserName != null)
                {
                    await _groupService.AddUserToGroup(user.Id, newGroupId);
                }
            }
            return Page();
        }

        //public async Task<IActionResult> OnPostAddUserToGroupAsync(string userId, int groupId)
        //{
        //    if(ModelState.IsValid)
        //    {
        //    }
        //}
    }
}
