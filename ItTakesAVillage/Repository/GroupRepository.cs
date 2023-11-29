using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using Microsoft.EntityFrameworkCore;

namespace ItTakesAVillage.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ItTakesAVillageContext _context;
        public GroupRepository(ItTakesAVillageContext context)
        {
            _context = context;
        }
        public async Task AddGroupAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
        }
        public async Task AddUserToGroupAsync(UserGroup userGroup)
        {
            await _context.UserGroups.AddAsync(userGroup);
        }

        public async Task<List<UserGroup>> GetUserGroupsAsync()
        {
            return await _context.UserGroups.ToListAsync();
        }
        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
