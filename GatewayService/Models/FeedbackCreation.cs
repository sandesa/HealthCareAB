namespace GatewayService.Models
{
    public class FeedbackCreation
    {
        public int BookingId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
