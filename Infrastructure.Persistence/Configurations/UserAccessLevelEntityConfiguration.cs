using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class UserAccessLevelEntityConfiguration : IEntityTypeConfiguration<UserAccessLevel>
    {
        public void Configure(EntityTypeBuilder<UserAccessLevel> builder)
        {
            builder.HasKey(sc => new { sc.AccessLevelId, sc.UserId });

            builder.ToTable(nameof(UserAccessLevel));

            builder.HasOne(a => a.User)
                .WithMany(a => a.UserAccessLevels)
                .HasForeignKey(a => a.UserId);

            builder.HasOne(a => a.AccessLevel)
                .WithMany(a => a.UserAccessLevels)
                .HasForeignKey(a => a.AccessLevelId);

            builder.HasData(new UserAccessLevel { UserId = 1, AccessLevelId = 1 });
            builder.HasData(new UserAccessLevel { UserId = 1, AccessLevelId = 2 });
            builder.HasData(new UserAccessLevel { UserId = 2, AccessLevelId = 1 });
            builder.HasData(new UserAccessLevel { UserId = 2, AccessLevelId = 2 });
            builder.HasData(new UserAccessLevel { UserId = 3, AccessLevelId = 1 });
        }
    }
}
