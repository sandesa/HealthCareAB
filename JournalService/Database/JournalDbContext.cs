using JournalService.Models;
using Microsoft.EntityFrameworkCore;

namespace JournalService.Database
{
    public class JournalDbContext : DbContext
    {
        public JournalDbContext(DbContextOptions<JournalDbContext> options) : base(options) { }

        public DbSet<Journal> Journals { get; set; }
    }
}
