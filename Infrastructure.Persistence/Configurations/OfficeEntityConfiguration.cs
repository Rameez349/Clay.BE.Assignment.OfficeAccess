using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OfficeEntityConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable(nameof(Office));

            builder.HasData(new Office { Id = 1, Name = "Office One" });
            builder.HasData(new Office { Id = 2, Name = "Office Two" });
        }
    }
}
