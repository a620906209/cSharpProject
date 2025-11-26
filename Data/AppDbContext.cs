using Microsoft.EntityFrameworkCore;

namespace SimpleApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<HelloMessage> HelloMessages => Set<HelloMessage>();
}
