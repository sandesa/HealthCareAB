namespace FeedbackService.Models
{
    public class FeedbackCreate
    {
        public int BookingId { get; set; }
        public int Rating { get; set; }
        public string? Message { get; set; }
        public DateTime Created { get; set; }
    }
}
