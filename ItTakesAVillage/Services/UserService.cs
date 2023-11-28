using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using Microsoft.EntityFrameworkCore;

namespace ItTakesAVillage.Services
{
    public class UserService : IUserService
    {
        private readonly ItTakesAVillageContext _context;

        public UserService(ItTakesAVillageContext context)
        {
            _context = context;
        }
        //TODO: Fixa nullvarning
        public async Task<ItTakesAVillageUser> GetUserById(string userId) => await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

    }
}
