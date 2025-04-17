using AvailabilityService.Models;
using Microsoft.EntityFrameworkCore;

namespace AvailabilityService.Database
{
    public class AvailabilityDbContext : DbContext
    {
        public AvailabilityDbContext(DbContextOptions<AvailabilityDbContext> options) : base(options) { }

        public DbSet<Availability> Availabilities { get; set; }
    }
}
