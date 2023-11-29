using ItTakesAVillage.Models;

namespace ItTakesAVillage.Contracts
{
    public interface IUserRepository
    {
        Task<ItTakesAVillageUser> GetByIdAsync(string userName);
    }
}
