namespace ItTakesAVillage.Models
{
    public class BaseEvent
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public DateTime DateTime { get; set; }
        public string? Message { get; set; }
    }
}