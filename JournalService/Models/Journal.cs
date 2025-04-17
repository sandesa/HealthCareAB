namespace JournalService.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int CaregiverId { get; set; }
        public int? BookingId { get; set; }
        public required string JournalType { get; set; }
        public required string JournalEntry { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
