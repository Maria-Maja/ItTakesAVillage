namespace ItTakesAVillage.Contracts
{
    public interface IGroupService
    {
        Task<int> SaveGroup(Models.Group group);
        Task AddUserToGroup(string userId, int groupId);
    }
}
