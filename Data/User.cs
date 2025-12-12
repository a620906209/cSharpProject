using System.Collections.Generic;

namespace SimpleApi.Data;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<HelloMessage> HelloMessages { get; set; } = new();
}
