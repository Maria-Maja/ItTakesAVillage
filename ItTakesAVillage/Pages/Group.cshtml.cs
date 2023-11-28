using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace ItTakesAVillage.Pages
{
    public class GroupModel : PageModel
    {
        private readonly IGroupService _groupService;
        private readonly UserManager<ItTakesAVillageUser> _userManager;

        public ItTakesAVillageUser? CurrentUser { get; set; }

        [BindProperty]
        public Models.Group NewGroup { get; set; } = new Models.Group();

        [BindProperty]
        public UserGroup NewUserGroup { get; set; } = new UserGroup();

        public GroupModel(IGroupService groupService, UserManager<ItTakesAVillageUser> userManager)
        {
            _groupService = groupService;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            ViewData["UserId"] = new SelectList(_userManager.Users, "Id", "Email");
            ViewData["GroupId"] = new SelectList(await _groupService.GetAll(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostNewGroupAsync()
        {
            if (ModelState.IsValid)
            {
                CurrentUser = await _userManager.GetUserAsync(User);
                var newGroupId = await _groupService.Save(NewGroup);
                if (CurrentUser != null && newGroupId != 0)
                {
                    await _groupService.AddUser(CurrentUser.Id, newGroupId);
                }
            }
            return RedirectToPage("/Group");
        }

        public async Task<IActionResult> OnPostAddUserToGroupAsync()
        {
            if (ModelState.IsValid)
            {
                await _groupService.AddUser(NewUserGroup.UserId, NewUserGroup.GroupId);
            }
            return RedirectToPage("/Group");
        }

    }
}
