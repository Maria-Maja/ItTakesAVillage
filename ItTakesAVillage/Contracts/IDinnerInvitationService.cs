using ItTakesAVillage.Models;

namespace ItTakesAVillage.Contracts
{
    public interface IDinnerInvitationService
    {
        Task<bool> Create(DinnerInvitation invitation);
        Task<List<DinnerInvitation>> GetAll();
    }
}
