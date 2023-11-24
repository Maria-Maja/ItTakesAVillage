namespace ItTakesAVillage.Models
{
    public class DinnerInvitation
    {
        public int GroupId { get; set; }
        public string UserId { get; set; } //Id of the user that creates the invitation
        public string Course { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
    }
}
