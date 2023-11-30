using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using ItTakesAVillage.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ItTakesAVillage.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepository<Group> _groupRepository;
        private readonly IRepository<ItTakesAVillageUser> _userRepository;
        private readonly IRepository<UserGroup> _userGroupRepository;

        public GroupService(IRepository<Group> groupRepository, 
            IRepository<ItTakesAVillageUser> userRepository,
            IRepository<UserGroup> userGroupRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _userGroupRepository = userGroupRepository;
        }

        public async Task<int> Save(Group group, string userId)
        {
            var groupsByUserId = await GetGroupsByUserId(userId);

            if (groupsByUserId.Any(x => x != null && x.Name == group.Name))
                return 0;

            await _groupRepository.AddAsync(group);

            return group.Id;
        }

        public async Task<bool> AddUser(string userId, int groupId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var usergroups = await _groupRepository.GetUserGroupsAsync();

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

            await _groupRepository.AddUserAsync(userGroup);
            await _groupRepository.SaveChangesAsync();

            return true;
        }

        public async Task<List<ItTakesAVillageUser?>> GetMembers(int groupId)
        {
            var userGroups = await _groupRepository.GetUserGroupsAsync();

            var groupMembers = userGroups
                .Where(x => x.GroupId == groupId)
                .Select(x => x.User).ToList();

            return groupMembers;
        }

        public async Task<List<Group?>> GetGroupsByUserId(string userId)
        {
            var userGroups = await _userGroupRepository.GetByFilterAsync(x => x.UserId == userId);

            return userGroups.Select(x => x.Group).ToList();
        }
    }
}
