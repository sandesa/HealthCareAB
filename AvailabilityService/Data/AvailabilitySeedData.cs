using AvailabilityService.Database;
using AvailabilityService.Models;
using Microsoft.EntityFrameworkCore;

namespace AvailabilityService.Data
{
    public class AvailabilitySeedData
    {
        public static async Task InitializeAsync(AvailabilityDbContext context, bool reseed, CancellationToken token)
        {
            if (reseed && context.Availabilities.Any())
            {
                context.RemoveRange(context.Availabilities);
                context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Availabilities', RESEED, 0)");
                await context.SaveChangesAsync(token);
            }

            if (!context.Availabilities.Any())
            {
                await context.Availabilities.AddRangeAsync(
                    new Availability
                    {
                        CaregiverId = 1,
                        StartTime = DateTime.UtcNow.AddDays(5),
                        EndTime = DateTime.UtcNow.AddDays(5).AddHours(8),
                        Notes = "Available for 8 hours",
                        CreatedAt = DateTime.UtcNow,
                    },
                    new Availability
                    {
                        CaregiverId = 1,
                        StartTime = DateTime.UtcNow.AddDays(5),
                        EndTime = DateTime.UtcNow.AddDays(5).AddHours(8),
                        Notes = "Available for 8 hours",
                        CreatedAt = DateTime.UtcNow,
                    },
                    new Availability
                    {
                        CaregiverId = 2,
                        StartTime = new DateTime(2025, 10, 01),
                        EndTime = new DateTime(2025, 10, 01).AddHours(8),
                        CreatedAt = DateTime.UtcNow,
                    },
                    new Availability
                    {
                        CaregiverId = 3,
                        StartTime = DateTime.UtcNow.AddDays(20),
                        EndTime = DateTime.UtcNow.AddDays(20).AddHours(8),
                        CreatedAt = DateTime.UtcNow,
                    },
                    new Availability
                    {
                        CaregiverId = 4,
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow.AddHours(8),
                        Notes = "Available for 8 hours",
                        CreatedAt = DateTime.UtcNow,
                    },
                    new Availability
                    {
                        CaregiverId = 4,
                        StartTime = DateTime.UtcNow.AddDays(12),
                        EndTime = DateTime.UtcNow.AddDays(12).AddHours(5),
                        Notes = "Available for 5 hours",
                        CreatedAt = DateTime.UtcNow,
                    },
                    new Availability
                    {
                        CaregiverId = 2,
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow.AddDays(5),
                        Notes = "Available for 5 days",
                        CreatedAt = DateTime.UtcNow,
                    },
                    new Availability
                    {
                        CaregiverId = 16,
                        StartTime = DateTime.UtcNow.AddDays(30),
                        EndTime = DateTime.UtcNow.AddDays(30).AddHours(8),
                        Notes = "Available in 30 days",
                        CreatedAt = DateTime.UtcNow,
                    },
                    new Availability
                    {
                        CaregiverId = 7,
                        StartTime = DateTime.UtcNow.AddDays(7),
                        EndTime = DateTime.UtcNow.AddDays(7).AddHours(8),
                        Notes = "Available for 8 hours",
                        CreatedAt = DateTime.UtcNow,
                    }
                );
                await context.SaveChangesAsync(token);
            }
        }
    }
}
