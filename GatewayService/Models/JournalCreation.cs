namespace GatewayService.Models
{
    public class JournalCreation
    {
        public int PatientId { get; set; }
        public int CaregiverId { get; set; }
        public int? BookingId { get; set; }
        public required string JournalType { get; set; }
        public required string JournalEntry { get; set; }
    }
}
