using ItTakesAVillage.Models;

namespace ItTakesAVillage.Contracts
{
    public interface IUserService
    {
        Task<ItTakesAVillageUser> GetById(string userName);
    }
}
