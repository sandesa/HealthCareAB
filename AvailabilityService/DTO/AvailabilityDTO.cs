namespace AvailabilityService.DTO
{
    public class AvailabilityDTO
    {
        public int CaregiverId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Notes { get; set; }
    }
}
