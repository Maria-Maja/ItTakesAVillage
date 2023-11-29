using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using ItTakesAVillage.Repository;
using Microsoft.EntityFrameworkCore;

namespace ItTakesAVillage.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Group>> GetAll()
        {
            return await _groupRepository.GetAllGroupsAsync();
        }

        public async Task<int> Save(Group group)
        {
            await _groupRepository.AddGroupAsync(group);
            await _groupRepository.SaveChangesAsync();

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

            await _groupRepository.AddUserToGroupAsync(userGroup);
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
    }
}
