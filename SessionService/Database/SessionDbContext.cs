using Microsoft.EntityFrameworkCore;
using SessionService.Models;

namespace SessionService.Database
{
    public class SessionDbContext : DbContext
    {
        public SessionDbContext(DbContextOptions<SessionDbContext> options) : base(options) { }

        public DbSet<Session> Sessions { get; set; }
    }
}
