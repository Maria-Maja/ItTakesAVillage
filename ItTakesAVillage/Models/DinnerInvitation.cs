namespace ItTakesAVillage.Models
{
    public class DinnerInvitation
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string UserId { get; set; } = string.Empty; //Id of the user that creates the invitation
        public string? Course { get; set; }  
        public DateTime DateTime { get; set; }
        public string? Location { get; set; }
    }
}
