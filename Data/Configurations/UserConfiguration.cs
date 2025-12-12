using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleApi.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = 1,
                Name = "測試用戶",
                Phone = "0900111222",
                Birthday = new DateTime(1992, 5, 10),
                UpdatedAt = new DateTime(2025, 11, 28, 15, 0, 0)
            },
            new User
            {
                Id = 2,
                Name = "行政助理",
                Phone = "0922333444",
                Birthday = new DateTime(1988, 4, 4),
                UpdatedAt = new DateTime(2025, 11, 28, 15, 0, 0)
            });
    }
}
