using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class AccessHistoryEntityConfiguration : IEntityTypeConfiguration<AccessHistory>
    {
        public void Configure(EntityTypeBuilder<AccessHistory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable(nameof(AccessHistory));

            builder.HasOne(a => a.Door);
            builder.HasOne(a => a.User);
        }
    }
}
