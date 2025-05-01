namespace BookingService.Models
{
    public class BookingCreation
    {
        public int CaregiverId { get; set; }
        public DateTime? MeetingDate { get; set; }
        public string? MeetingType { get; set; }
        public string? Clinic { get; set; }
        public string? Address { get; set; }
    }
}
