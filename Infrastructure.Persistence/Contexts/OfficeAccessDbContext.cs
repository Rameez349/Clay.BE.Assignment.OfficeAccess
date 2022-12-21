using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Infrastructure.Persistence.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Contexts
{
    public class OfficeAccessDbContext : DbContext
    {
        private readonly DbOptions _dbOptions;
        public OfficeAccessDbContext(IOptions<DbOptions> option)
        {
            _dbOptions = option.Value;
        }

        public virtual DbSet<Office> Offices { get; set; } = null!;
        public virtual DbSet<Door> Doors { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<DoorAccessLevel> DoorAccessLevels { get; set; } = null!;
        public virtual DbSet<UserAccessLevel> UserAccessLevels { get; set; } = null!;
        public DbSet<AccessHistory> AccessHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_dbOptions.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new OfficeEntityConfiguration().Configure(modelBuilder.Entity<Office>());
            new DoorEntityConfiguration().Configure(modelBuilder.Entity<Door>());
            new UserEntityConfiguration().Configure(modelBuilder.Entity<User>());
            new DoorAccessLevelEntityConfiguration().Configure(modelBuilder.Entity<DoorAccessLevel>());
            new UserAccessLevelEntityConfiguration().Configure(modelBuilder.Entity<UserAccessLevel>());
            new AccessLevelEntityConfiguration().Configure(modelBuilder.Entity<AccessLevel>());
            new AccessHistoryEntityConfiguration().Configure(modelBuilder.Entity<AccessHistory>());
        }
    }
}
