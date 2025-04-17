namespace JournalService.DTO
{
    public class JournalDTO
    {
        public int PatientId { get; set; }
        public int CaregiverId { get; set; }
        public int? BookingId { get; set; }
        public string JournalType { get; set; } = "";
        public string JournalText { get; set; } = "";
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
