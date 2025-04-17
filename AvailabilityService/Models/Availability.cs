namespace AvailabilityService.Models
{
    public class Availability
    {
        public int Id { get; set; }
        public int CaregiverId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
