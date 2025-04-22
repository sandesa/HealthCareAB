using FeedbackService.Database;
using FeedbackService.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedbackService.Data
{
    public class FeedbackSeedData
    {
        public static async Task InitializeAsync(FeedbackDbContext context, bool reseed, CancellationToken token)
        {
            if (reseed && context.Feedbacks.Any())
            {
                context.RemoveRange(context.Feedbacks);
                context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Feedbacks', RESEED, 0)");
                await context.SaveChangesAsync(token);
            }

            if (!context.Feedbacks.Any())
            {
                await context.Feedbacks.AddRangeAsync(
                    new Feedback
                    {
                        BookingId = 1,
                        Rating = 5,
                        Comment = "Excellent service!",
                        Created = DateTime.UtcNow,
                    },
                    new Feedback
                    {
                        BookingId = 2,
                        Rating = 2,
                        Comment = "Bad service!",
                        Created = DateTime.UtcNow,
                    },
                    new Feedback
                    {
                        BookingId = 3,
                        Rating = 4,
                        Comment = "Good service!",
                        Created = DateTime.UtcNow,
                    },
                    new Feedback
                    {
                        BookingId = 4,
                        Rating = 3,
                        Comment = "Decent service!",
                        Created = DateTime.UtcNow,
                    },
                    new Feedback
                    {
                        BookingId = 5,
                        Rating = 5,
                        Comment = "Excellent service!",
                        Created = DateTime.UtcNow,
                    },
                    new Feedback
                    {
                        BookingId = 6,
                        Rating = 1,
                        Comment = "Super bad service!",
                        Created = DateTime.UtcNow,
                    },
                    new Feedback
                    {
                        BookingId = 7,
                        Rating = 2,
                        Comment = "Bad service!",
                        Created = DateTime.UtcNow,
                    },
                    new Feedback
                    {
                        BookingId = 8,
                        Rating = 5,
                        Comment = "Excellent service!",
                        Created = DateTime.UtcNow,
                    },
                    new Feedback
                    {
                        BookingId = 9,
                        Rating = 4,
                        Comment = "Good service!",
                        Created = DateTime.UtcNow,
                    },
                    new Feedback
                    {
                        BookingId = 10,
                        Rating = 5,
                        Comment = "Excellent service!",
                        Created = DateTime.UtcNow,
                    }
                );
                await context.SaveChangesAsync(token);
            }
        }
    }
}
