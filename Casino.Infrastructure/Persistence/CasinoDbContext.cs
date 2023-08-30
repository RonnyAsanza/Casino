using Casino.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Casino.Infrastructure.Persistence
{
    public class CasinoDbContext : DbContext
    {
        private readonly string _connectionString = "";
        public CasinoDbContext(DbContextOptions<CasinoDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<SeedingEntry>()
            .Property(s => s.DateCreatedUtc)
            .HasDefaultValueSql("GETDATE()");

            base.OnModelCreating(builder);
        }

        internal virtual DbSet<SeedingEntry> SeedingEntries { get; set; }

        public DbSet<User> User { get; set; }
    }
}
