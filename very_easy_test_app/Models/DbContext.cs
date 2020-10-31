using Microsoft.EntityFrameworkCore;
using very_easy_test_app.Models.Entities;
using very_easy_test_app.Models.EntityConfigure;

namespace very_easy_test_app.Models
{
    public sealed class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<HomeEntity> HomeOwer { get; set; }
        public DbSet<HomeOwenerEntity> HomeOwener { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("HouseManagement");
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new EntityConfigureHome());
            modelBuilder.ApplyConfiguration(new EntityConfigureBase<HomeOwenerEntity>());
        }
    }
}