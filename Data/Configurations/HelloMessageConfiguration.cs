using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleApi.Data.Configurations;

public class HelloMessageConfiguration : IEntityTypeConfiguration<HelloMessage>
{
    public void Configure(EntityTypeBuilder<HelloMessage> builder)
    {
        builder
            .HasOne(h => h.User)
            .WithMany(u => u.HelloMessages)
            .HasForeignKey(h => h.UserId);
    }
}
