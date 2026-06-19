using Microsoft.EntityFrameworkCore;
using ImpedexTracker.Models;

namespace ImpedexTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Job> Jobs { get; set; }
    }
}