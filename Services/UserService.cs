using SimpleApi.Data;
using SimpleApi.Dtos.Requests;
using SimpleApi.Repositories;

namespace SimpleApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<IReadOnlyList<User>> GetAllAsync()
    {
        return _userRepository.GetAllAsync();
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return _userRepository.GetByIdAsync(id);
    }

    public async Task<User> CreateAsync(CreateUserRequestDto request)
    {
        var user = new User
        {
            Name = request.Name,
            Phone = request.Phone,
            Birthday = request.Birthday ?? default,
            UpdatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);
        return user;
    }

    public Task<IReadOnlyList<User>> SearchByNameAsync(string term)
    {
        return _userRepository.SearchByNameAsync(term);
    }

    public Task<IReadOnlyList<HelloMessage>> GetMessagesByUserAsync(int userId)
    {
        return _userRepository.GetMessagesByUserAsync(userId);
    }

    public Task<User?> GetUserWithMessagesAsync(int userId)
    {
        return _userRepository.GetUserWithMessagesAsync(userId);
    }
}
