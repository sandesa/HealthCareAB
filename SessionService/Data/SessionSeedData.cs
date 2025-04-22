using Microsoft.EntityFrameworkCore;
using SessionService.Database;
using SessionService.Models;

namespace SessionService.Data
{
    public class SessionSeedData
    {
        public static async Task InitializeAsync(SessionDbContext context, bool reseed, CancellationToken token)
        {
            if (reseed && context.Sessions.Any())
            {
                context.RemoveRange(context.Sessions);
                context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT ('Sessions', RESEED, 0)");
                await context.SaveChangesAsync(token);
            }

            if (!context.Sessions.Any())
            {
                await context.Sessions.AddRangeAsync(
                    new Session
                    {
                        Email = "test1.testsson@gmail.com",
                        AccessToken = "test1Token",
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        Login = DateTime.UtcNow,
                    },
                    new Session
                    {
                        Email = "test2.testsson@gmail.com",
                        AccessToken = "test2Token",
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        Login = DateTime.UtcNow,
                    },
                    new Session
                    {
                        Email = "test3.testsson@gmail.com",
                        AccessToken = "test3Token",
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        Login = DateTime.UtcNow,
                    },
                    new Session
                    {
                        Email = "test4.testsson@gmail.com",
                        AccessToken = "test4Token",
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        Login = DateTime.UtcNow,
                    },
                    new Session
                    {
                        Email = "test5.testsson@gmail.com",
                        AccessToken = "test5Token",
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        Login = DateTime.UtcNow,
                    }
                );
                await context.SaveChangesAsync(token);
            }
        }
    }
}
