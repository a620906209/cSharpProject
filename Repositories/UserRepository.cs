using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;

namespace SimpleApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        return await _context.Users
            .OrderBy(u => u.Name)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<User>> SearchByNameAsync(string term)
    {
        return await _context.Users
            .Where(u => EF.Functions.Like(u.Name, $"%{term}%"))
            .OrderBy(u => u.Name)
            .ToListAsync();
    }
}
