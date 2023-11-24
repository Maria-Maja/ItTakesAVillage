using ItTakesAVillage.Models;

namespace ItTakesAVillage.Contracts
{
    public interface IUserService
    {
        ItTakesAVillageUser GetUserByUserName(string userName);
    }
}
