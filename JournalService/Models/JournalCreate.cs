namespace JournalService.Models
{
    public class JournalCreate
    {
        public int PatientId { get; set; }
        public int CaregiverId { get; set; }
        public int? BookingId { get; set; }
        public required string JournalType { get; set; }
        public required string JournalText { get; set; }
    }
}
