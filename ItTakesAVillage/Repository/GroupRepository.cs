using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ItTakesAVillage.Repository
{
    public class GroupRepository : IRepository<Group>, IGroupRepository
    {
        private readonly ItTakesAVillageContext _context;
        public GroupRepository(ItTakesAVillageContext context)
        {
            _context = context;
        }
        public override async Task AddAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Group>> GetAsync() => await _context.Groups.ToListAsync();
        public async Task<List<Group>> GetAsync(int id) => await _context.Groups.ToListAsync();
        public async Task<List<Group>> GetByFilterAsync(Expression<Func<Group, bool>> expression)
        {
            return await _context.Groups.Where(expression).ToListAsync();
        }

        //Delete
        //Update

        public async Task AddUserAsync(UserGroup userGroup)
        {
            await _context.UserGroups.AddAsync(userGroup);
            await _context.SaveChangesAsync();
        }
        public async Task<List<UserGroup>> GetUserGroupsAsync() //TODO flytta till user?
        {
            return await _context.UserGroups.ToListAsync();
        }
        public async Task SaveChangesAsync()
        {
        }

        

    }
}
