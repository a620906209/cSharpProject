using SimpleApi.Services;

namespace SimpleApi.Services;

public class UserGreetingService : IUserGreetingService
{
    public string Greet(string name) => $"Hello, {name}！目前時間：{DateTime.UtcNow:yyyy/MM/dd HH:mm}";
}
