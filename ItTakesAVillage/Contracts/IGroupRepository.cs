using ItTakesAVillage.Models;

namespace ItTakesAVillage.Contracts
{
    public interface IGroupRepository
    {
        Task<List<Models.UserGroup>> GetUserGroupsAsync();
        Task<List<Group>> GetAllGroupsAsync();
        Task AddAsync(Group group);
        Task AddUserAsync(UserGroup userGroup);
        Task SaveChangesAsync();
    }
}
