using ItTakesAVillage.Models;

namespace ItTakesAVillage.Contracts
{
    public interface IDinnerInvitationService
    {
        Task Create(DinnerInvitation invitation);
        Task<List<DinnerInvitation>> GetAll();
    }
}
