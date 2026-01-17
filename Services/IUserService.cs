using SimpleApi.Data;
using SimpleApi.Dtos.Requests;

namespace SimpleApi.Services;

public interface IUserService
{
    Task<IReadOnlyList<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(CreateUserRequestDto request);
    Task<IReadOnlyList<User>> SearchByNameAsync(string term);
    Task<IReadOnlyList<HelloMessage>> GetMessagesByUserAsync(int userId);
    Task<User?> GetUserWithMessagesAsync(int userId);
}
