using BookingService.Database;
using BookingService.Models;
using BookingService.Utilities.StdDef;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data
{
    public class BookingSeedData
    {
        public static async Task InitializeAsync(BookingDbContext context, bool reseed, CancellationToken token)
        {
            if (reseed && context.Bookings.Any())
            {
                context.RemoveRange(context.Bookings);
                context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Bookings', RESEED, 0)");
                await context.SaveChangesAsync(token);
            }

            if (!context.Bookings.Any())
            {
                await context.Bookings.AddRangeAsync(
                    new Booking
                    {
                        CaregiverId = 1,
                        PatientId = 1,
                        MeetingDate = DateTime.UtcNow.AddDays(10),
                        MeetingType = MeetingType.InitialConsultation.ToString(),
                        Clinic = "Main Clinic",
                        Address = "Testgatan 1, 41234 Gothenburg",
                        IsCancelled = false,
                        Created = DateTime.UtcNow,
                    },

                    new Booking
                    {
                        CaregiverId = 1,
                        PatientId = 1,
                        MeetingDate = DateTime.UtcNow.AddDays(20),
                        MeetingType = MeetingType.FollowUp.ToString(),
                        Clinic = "Main Clinic",
                        Address = "Testgatan 1, 41234 Gothenburg",
                        IsCancelled = false,
                        Created = DateTime.UtcNow,
                    },

                    new Booking
                    {
                        CaregiverId = 5,
                        PatientId = 3,
                        MeetingDate = DateTime.UtcNow.AddDays(4),
                        MeetingType = MeetingType.Emergency.ToString(),
                        Clinic = "Main Clinic",
                        Address = "Testgatan 1, 41234 Gothenburg",
                        IsCancelled = false,
                        Created = DateTime.UtcNow,
                    },

                    new Booking
                    {
                        CaregiverId = 2,
                        PatientId = 3,
                        MeetingDate = DateTime.UtcNow.AddDays(7),
                        MeetingType = MeetingType.FollowUp.ToString(),
                        Clinic = "Main Clinic",
                        Address = "Testgatan 1, 41234 Gothenburg",
                        IsCancelled = false,
                        Created = DateTime.UtcNow,
                    },

                    new Booking
                    {
                        CaregiverId = 1,
                        PatientId = 10,
                        MeetingDate = DateTime.UtcNow.AddDays(22),
                        MeetingType = MeetingType.InitialConsultation.ToString(),
                        Clinic = "Main Clinic",
                        Address = "Testgatan 1, 41234 Gothenburg",
                        IsCancelled = false,
                        Created = DateTime.UtcNow,
                    },

                    new Booking
                    {
                        CaregiverId = 1,
                        PatientId = 11,
                        MeetingDate = DateTime.UtcNow.AddDays(2),
                        MeetingType = MeetingType.Other.ToString(),
                        Clinic = "Main Clinic",
                        Address = "Testgatan 1, 41234 Gothenburg",
                        IsCancelled = false,
                        Created = DateTime.UtcNow,
                    },

                    new Booking
                    {
                        CaregiverId = 1,
                        PatientId = 11,
                        MeetingDate = DateTime.UtcNow.AddDays(6),
                        MeetingType = MeetingType.InitialConsultation.ToString(),
                        Clinic = "Main Clinic",
                        Address = "Testgatan 1, 41234 Gothenburg",
                        IsCancelled = false,
                        Created = DateTime.UtcNow,
                    }
                );
                await context.SaveChangesAsync(token);
            }
        }
    }
}
