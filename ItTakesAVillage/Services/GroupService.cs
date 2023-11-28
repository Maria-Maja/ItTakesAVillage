using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using Microsoft.EntityFrameworkCore;

namespace ItTakesAVillage.Services
{
    public class GroupService : IGroupService
    {
        private readonly ItTakesAVillageContext _context;

        public GroupService(ItTakesAVillageContext context)
        {
            _context = context;
        }

        public async Task<List<Group>> GetAll()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<int> Save(Group group)
        {
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();

            return group.Id;
        }      

        public async Task AddUser(string userId, int groupId)
        {
            var userGroup = new UserGroup
            {
                UserId = userId,
                GroupId = groupId
            };

            await _context.UserGroups.AddAsync(userGroup);
            await _context.SaveChangesAsync();
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
