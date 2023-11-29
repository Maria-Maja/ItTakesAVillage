using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using Microsoft.EntityFrameworkCore;

namespace ItTakesAVillage.Services
{
    public class GroupService : IGroupService
    {
        private readonly IItTakesAVillageContext _context;
        private readonly IUserService _userService;

        public GroupService(IItTakesAVillageContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<List<Group>> GetAll()
        {
            return await _context.Groups.ToListAsync();
        }

        public IQueryable<UserGroup> GetAllUserGroups() => _context.UserGroups;

        public async Task<int> Save(Group group)
        {
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();

            return group.Id;
        }

        public async Task<bool> AddUser(string userId, int groupId)
        {
            var user = await _userService.GetById(userId);
            var usergroups = GetAllUserGroups().ToListAsync();

            bool userExistsInList = usergroups.Any(x => x.UserId == user.Id);

            if (userExistsInList)
            {
                return false;
            }

            var userGroup = new UserGroup
            {
                UserId = userId,
                GroupId = groupId
            };

            await _context.UserGroups.AddAsync(userGroup);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ItTakesAVillageUser?>> GetMembers(int groupId)
        {
            var groupMembers = await _context.UserGroups
                .Where(x => x.GroupId == groupId)
                .Select(x => x.User)
                .ToListAsync();

            return groupMembers;
        }
    }
}
