using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;
using Microsoft.EntityFrameworkCore;

namespace ItTakesAVillage.Services
{
    public class DinnerInvitationService : IDinnerInvitationService
    {
        private readonly ItTakesAVillageContext _context;

        public DinnerInvitationService(ItTakesAVillageContext context)
        {
            _context = context;
        }

        public async Task<List<DinnerInvitation>> GetAll()
        {
            return await _context.Events
                .OfType<DinnerInvitation>()
                .ToListAsync();
        }

        public async Task Create(DinnerInvitation invitation)
        {
            await _context.Events.AddAsync(invitation);
            await _context.SaveChangesAsync();
        }
    }
}
