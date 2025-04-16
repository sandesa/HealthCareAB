namespace FeedbackService.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int Rating { get; set; }
        public string? Message { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
