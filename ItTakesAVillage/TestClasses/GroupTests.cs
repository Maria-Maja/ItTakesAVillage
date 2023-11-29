using ItTakesAVillage.Contracts;

namespace ItTakesAVillage.TestClasses
{
    public class GroupTests
    {
        private readonly IGroupService _groupService;

        public GroupTests(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public async Task<bool> AddUser(string userId, int groupId) => await _groupService.AddUser(userId,groupId);
    }
}
