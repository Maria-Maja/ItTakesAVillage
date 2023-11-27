using ItTakesAVillage.Models;

namespace ItTakesAVillage.Contracts
{
    public interface IUserService
    {
        Task<ItTakesAVillageUser> GetUserById(string userName);
    }
}
