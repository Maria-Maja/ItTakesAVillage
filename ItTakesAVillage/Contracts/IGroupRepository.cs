using ItTakesAVillage.Models;

namespace ItTakesAVillage.Contracts
{
    public interface IGroupRepository
    {
        Task<List<Models.UserGroup>> GetUserGroupsAsync();
        Task<List<Group>> GetAllGroupsAsync();
        Task AddGroupAsync(Group group);
        Task AddUserToGroupAsync(UserGroup userGroup);
        Task SaveChangesAsync();
    }
}
