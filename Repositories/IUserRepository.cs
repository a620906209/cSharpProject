using SimpleApi.Data;

namespace SimpleApi.Repositories;

public interface IUserRepository
{
    Task<IReadOnlyList<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
    Task<IReadOnlyList<User>> SearchByNameAsync(string term);
}
