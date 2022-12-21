using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AccessLevelEntityConfiguration : IEntityTypeConfiguration<AccessLevel>
    {
        public void Configure(EntityTypeBuilder<AccessLevel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable(nameof(AccessLevel));

            builder.HasData(new AccessLevel { Id = 1, Name = "General" });
            builder.HasData(new AccessLevel { Id = 2, Name = "Special" });
        }
    }
}
