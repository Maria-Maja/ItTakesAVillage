using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;

namespace ItTakesAVillage.Services
{
    public class UserService : IUserService
    {
        private readonly ItTakesAVillageContext _context;

        public UserService(ItTakesAVillageContext context)
        {
            _context = context;
        }
        public ItTakesAVillageUser GetUserByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == userName);
            //TODO: Fixa nullvarning
        }
    }
}
