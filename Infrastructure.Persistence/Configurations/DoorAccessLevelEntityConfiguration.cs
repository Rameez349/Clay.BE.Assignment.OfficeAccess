using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DoorAccessLevelEntityConfiguration : IEntityTypeConfiguration<DoorAccessLevel>
    {
        public void Configure(EntityTypeBuilder<DoorAccessLevel> builder)
        {
            builder.HasKey(sc => new { sc.AccessLevelId, sc.DoorId });

            builder.ToTable(nameof(DoorAccessLevel));

            builder.HasOne(a => a.Door)
                .WithMany(a => a.DoorAccessLevels)
                .HasForeignKey(a => a.DoorId);

            builder.HasOne(a => a.AccessLevel)
                .WithMany(a => a.DoorAccessLevels)
                .HasForeignKey(a => a.AccessLevelId);

            builder.HasData(new DoorAccessLevel { AccessLevelId = 1, DoorId = 1 });
            builder.HasData(new DoorAccessLevel { AccessLevelId = 2, DoorId = 2 });
        }
    }
}
