namespace SimpleApi.Data;

public class HelloMessage
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime Uptime { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
