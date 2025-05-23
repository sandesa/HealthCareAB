﻿namespace BookingService.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int CaregiverId { get; set; }
        public int PatientId { get; set; }
        public DateTime? MeetingDate { get; set; }
        public string? MeetingType { get; set; }
        public string? Clinic { get; set; }
        public string? Address { get; set; }
        public bool IsCancelled { get; set; } = false;
        public DateTime? CancelDate { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
