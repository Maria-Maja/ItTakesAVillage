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
        private readonly IUserService _userService;
        private readonly UserManager<ItTakesAVillageUser> _userManager;
        private readonly ItTakesAVillageContext _context;

        //[BindProperty]
        public ItTakesAVillageUser CurrentUser { get; set; }
        [BindProperty]
        public Models.Group NewGroup { get; set; } = new Models.Group();
        
        [BindProperty]
        public Models.UserGroup NewUserGroup { get; set; } = new Models.UserGroup();

        public GroupModel(IGroupService groupService, IUserService userService, UserManager<ItTakesAVillageUser> userManager, ItTakesAVillageContext context)
        {
            _groupService = groupService;
            _userManager = userManager;
            _userService = userService;
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            ViewData["UserId"] = new SelectList(_userManager.Users, "Id", "Email");
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostNewGroupAsync()
        {
            if (ModelState.IsValid)
            {
                CurrentUser = await _userManager.GetUserAsync(User);
                var newGroupId = await _groupService.SaveGroup(NewGroup);
                if (newGroupId != 0 && CurrentUser.Id != null)
                {
                    await _groupService.AddUserToGroup(CurrentUser.Id, newGroupId);
                }
            }
            return RedirectToPage("/Group");
        }

        public async Task<IActionResult> OnPostAddUserToGroupAsync()
        {
            if (ModelState.IsValid)
            {
                await _groupService.AddUserToGroup(NewUserGroup.UserId, NewUserGroup.GroupId);
            }
            return RedirectToPage("/Group");
        }

    }
}
