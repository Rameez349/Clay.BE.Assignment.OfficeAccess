using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DoorEntityConfiguration : IEntityTypeConfiguration<Door>
    {
        public void Configure(EntityTypeBuilder<Door> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable(nameof(Door));

            builder.HasOne(o => o.Office)
                .WithMany(g => g.Doors)
                .HasForeignKey(d => d.OfficeId);

            builder.HasData(new Door { Id = 1, Name = "Entrance", OfficeId = 1 });
            builder.HasData(new Door { Id = 2, Name = "Storage", OfficeId = 1 });
        }
    }
}
