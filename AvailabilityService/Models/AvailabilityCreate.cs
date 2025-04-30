namespace AvailabilityService.Models
{
    public class AvailabilityCreate
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Notes { get; set; }
    }
}
