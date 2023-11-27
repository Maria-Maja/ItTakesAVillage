namespace ItTakesAVillage.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public DateTime DateTime { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }
}
