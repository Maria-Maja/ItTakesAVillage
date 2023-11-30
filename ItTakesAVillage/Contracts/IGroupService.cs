using ItTakesAVillage.Models;

namespace ItTakesAVillage.Contracts
{
    public interface IGroupService
    {
        Task<Group> Save(Group group, string userId);
        Task<bool> AddUser(string userId, int groupId);
        Task<List<ItTakesAVillageUser?>> GetMembers(int groupId);
    }
}
