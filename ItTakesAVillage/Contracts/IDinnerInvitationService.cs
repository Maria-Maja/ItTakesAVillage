using ItTakesAVillage.Models;

namespace ItTakesAVillage.Contracts
{
    public interface IDinnerInvitationService
    {
        Task CreateDinnerInvitation(DinnerInvitation invitation);
    }
}
