using ItTakesAVillage.Areas.Identity.Data;

namespace ItTakesAVillage.Models
{
    public class UserGroup
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "unknown";
        public int GroupId { get; set; }

        public virtual ItTakesAVillageUser? User { get; set; }
        public virtual Group? Group { get; set; }
    }
}
