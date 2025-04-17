namespace JournalService.Models
{
    public class JournalUpdate
    {
        public int? BookingId { get; set; }
        public string? JournalType { get; set; }
        public string? JournalEntry { get; set; }
    }
}
