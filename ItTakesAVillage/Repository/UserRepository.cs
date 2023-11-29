using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using Microsoft.EntityFrameworkCore;

namespace ItTakesAVillage.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ItTakesAVillageContext _context;

        public UserRepository(ItTakesAVillageContext context)
        {
            _context = context;
        }
        //TODO: Fixa nullvarning
        public async Task<ItTakesAVillageUser> GetByIdAsync(string userId) => await _context.Users.FindAsync(userId);

    }
}
