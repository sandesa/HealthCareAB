namespace GatewayService.Models
{
    public class AvailabilityUpdate
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Notes { get; set; }
    }
}
