namespace GatewayService.Models
{
    public class BookingUpdate
    {
        public DateTime? MeetingDate { get; set; }
        public string? MeetingType { get; set; }
        public string? Clinic { get; set; }
        public string? Address { get; set; }
    }
}
