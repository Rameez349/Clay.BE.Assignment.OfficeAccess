using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable(nameof(User));

            builder.HasData(new User { Id = 1, Name = "Rameez", AllowHistoryView = true });
            builder.HasData(new User { Id = 2, Name = "Darjan", AllowHistoryView = true });
            builder.HasData(new User { Id = 3, Name = "Lucas", AllowHistoryView = false });
        }
    }
}
