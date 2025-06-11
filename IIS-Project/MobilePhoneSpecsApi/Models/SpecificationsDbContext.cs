using Microsoft.EntityFrameworkCore;

namespace MobilePhoneSpecsApi.Models
{
    public class SpecificationsDbContext : DbContext
    {
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<PhoneDetails> PhoneDetails { get; set; }
        public DbSet<GsmLaunchDetails> GsmLaunchDetails { get; set; }
        public DbSet<GsmBodyDetails> GsmBodyDetails { get; set; }
        public DbSet<GsmDisplayDetails> GsmDisplayDetails { get; set; }
        public DbSet<GsmMemoryDetails> GsmMemoryDetails { get; set; }
        public DbSet<GsmSoundDetails> GsmSoundDetails { get; set; }
        public DbSet<GsmBatteryDetails> GsmBatteryDetails { get; set; }

        public SpecificationsDbContext() { }

        public SpecificationsDbContext(DbContextOptions<SpecificationsDbContext> options) : base(options) { }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("name=ConnectionStrings:msSql");
        }
    }
}
