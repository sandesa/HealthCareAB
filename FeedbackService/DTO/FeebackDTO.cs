namespace FeedbackService.DTO
{
    public class FeebackDTO
    {
        public int BookingId { get; set; }
        public int Rating { get; set; }
        public string? Message { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
