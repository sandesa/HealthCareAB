namespace BookingService.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int CaregiverId { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public string? MeetingType { get; set; }
        public string? Clinic { get; set; }
        public string? Address { get; set; }
    }
}
