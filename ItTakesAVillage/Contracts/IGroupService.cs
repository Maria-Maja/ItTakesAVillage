﻿using ItTakesAVillage.Models;

namespace ItTakesAVillage.Contracts
{
    public interface IGroupService
    {
        Task<int> Save(Group group);
        Task AddUser(string userId, int groupId);
        Task<List<ItTakesAVillageUser?>> GetMembers(int groupId);
        Task<List<Group>> GetAll();
    }
}
