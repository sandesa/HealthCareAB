using JournalService.Database;
using JournalService.Models;
using JournalService.Utilities.StdDef;
using Microsoft.EntityFrameworkCore;

namespace JournalService.Data
{
    public class JournalSeedData
    {
        public static async Task InitializeAsync(JournalDbContext context, bool reseed, CancellationToken token)
        {
            if (reseed && context.Journals.Any())
            {
                context.RemoveRange(context.Journals);
                context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Journals', RESEED, 0)");
                await context.SaveChangesAsync(token);
            }

            if (!context.Journals.Any())
            {
                await context.Journals.AddRangeAsync(
                    new Journal
                    {
                        PatientId = 1,
                        CaregiverId = 2,
                        BookingId = 1,
                        JournalType = JournalType.Observation.ToString(),
                        JournalText = "Patient is stable and recovering well.",
                        CreatedAt = DateTime.Now
                    },
                    new Journal
                    {
                        PatientId = 1,
                        CaregiverId = 2,
                        BookingId = 4,
                        JournalType = JournalType.Observation.ToString(),
                        JournalText = "Patient is stable and recovering well again.",
                        CreatedAt = DateTime.Now.AddDays(10)
                    },
                    new Journal
                    {
                        PatientId = 5,
                        CaregiverId = 4,
                        BookingId = 8,
                        JournalType = JournalType.Medication.ToString(),
                        JournalText = "New medication prescribed.",
                        CreatedAt = DateTime.Now.AddDays(20)
                    },
                    new Journal
                    {
                        PatientId = 30,
                        CaregiverId = 14,
                        BookingId = 15,
                        JournalType = JournalType.Treatment.ToString(),
                        JournalText = "New treatment plan.",
                        CreatedAt = DateTime.Now.AddDays(30)
                    },
                    new Journal
                    {
                        PatientId = 26,
                        CaregiverId = 3,
                        JournalType = JournalType.Observation.ToString(),
                        JournalText = "Patient is stable and recovering well.",
                        CreatedAt = DateTime.Now.AddDays(-10)
                    },
                    new Journal
                    {
                        PatientId = 2,
                        CaregiverId = 7,
                        JournalType = JournalType.Other.ToString(),
                        JournalText = "Thoughts and feelings.",
                        CreatedAt = DateTime.Now.AddDays(-15)
                    },
                    new Journal
                    {
                        PatientId = 8,
                        CaregiverId = 11,
                        JournalType = JournalType.Medication.ToString(),
                        JournalText = "New medication prescribed.",
                        CreatedAt = DateTime.Now.AddDays(-20)
                    },
                    new Journal
                    {
                        PatientId = 13,
                        CaregiverId = 4,
                        BookingId = 12,
                        JournalType = JournalType.Treatment.ToString(),
                        JournalText = "Treatment plan reworked.",
                        CreatedAt = DateTime.Now.AddDays(-3)
                    },
                    new Journal
                    {
                        PatientId = 1,
                        CaregiverId = 26,
                        BookingId = 65,
                        JournalType = JournalType.Observation.ToString(),
                        JournalText = "New observation.",
                        CreatedAt = DateTime.Now.AddDays(10)
                    },
                    new Journal
                    {
                        PatientId = 13,
                        CaregiverId = 4,
                        BookingId = 12,
                        JournalType = JournalType.Treatment.ToString(),
                        JournalText = "Treatment plan reworked.",
                        CreatedAt = DateTime.Now.AddDays(-3)
                    }
                );
                await context.SaveChangesAsync(token);
            }
        }
    }
}
