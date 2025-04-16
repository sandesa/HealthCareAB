namespace FeedbackService.DTO
{
    public class FeedbackDTO
    {
        public int BookingId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
