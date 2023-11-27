using ItTakesAVillage.Contracts;
using ItTakesAVillage.Data;
using ItTakesAVillage.Models;

namespace ItTakesAVillage.Services
{
    public class DinnerInvitationService : IDinnerInvitationService
    {
        private readonly ItTakesAVillageContext _context;

        public DinnerInvitationService(ItTakesAVillageContext context)
        {
            _context = context;
        }
        public async Task CreateDinnerInvitation(DinnerInvitation invitation)
        {
            await _context.Events.AddAsync(invitation);
            await _context.SaveChangesAsync();
        }
    }
}
