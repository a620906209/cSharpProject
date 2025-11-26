using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;

namespace SimpleApi.Controllers;

public class HelloRequest
{
    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }
}

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    private readonly AppDbContext _context;

    public HelloController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "你好，這是來自 C# API 的訊息" });
    }

    [HttpGet("{name}")]
    public IActionResult GetByName(string name)
    {
        return Ok(new
        {
            message = $"你好，{name}，歡迎1來到 C# API！"
        });
    }

    [HttpGet("greet")]
    public IActionResult Greet([FromQuery] string name, [FromQuery] int age)
    {
        return Ok(new
        {
            message = $"你好，{name}，你今年 {age} 歲！"
        });
    }

    [HttpGet("time")]
    public IActionResult GetServerTime()
    {
        return Ok(new
        {
            serverTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });
    }

    [HttpPost("create")]
    public IActionResult CreateHello([FromBody] HelloRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "Name 不可為空" });
        }

        return Ok(new
        {
            message = $"你好，{request.Name}，你今年 {request.Age} 歲，這是來自 POST API 的訊息！"
        });
    }

    [HttpGet("from-route/{name}")]
    public IActionResult FromRoute([FromRoute] string name)
    {
        return Ok(new
        {
            source = "route",
            name,
            message = $"參數是從路由 /hello/from-route/{{name}} 拿到的：{name}"
        });
    }

    [HttpGet("from-query")]
    public IActionResult FromQueryExplicit([FromQuery] string name)
    {
        return Ok(new
        {
            source = "query",
            name,
            message = $"參數是從 QueryString ?name=xxx 拿到的：{name}"
        });
    }

    [HttpPost("from-body")]
    public IActionResult FromBody([FromBody] HelloRequest request)
    {
        return Ok(new
        {
            source = "body",
            request.Name,
            request.Age,
            message = $"參數是從 JSON Body 拿到的：{request.Name}, {request.Age}"
        });
    }

    [HttpGet("messages")]
    public async Task<IActionResult> GetMessages()
    {
        var messages = await _context.HelloMessages
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new
            {
                m.Id,
                m.Content,
                m.CreatedAt
            })
            .ToListAsync();

        return Ok(messages);
    }

    [HttpPost("messages")]
    public async Task<IActionResult> CreateMessage([FromBody] HelloRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "Name 不可為空" });
        }

        var message = new HelloMessage
        {
            Content = $"{request.Name} ({request.Age} 歲) 來自當前時間 {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}",
            CreatedAt = DateTime.UtcNow
        };

        _context.HelloMessages.Add(message);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMessages), new { id = message.Id }, new
        {
            message.Id,
            message.Content,
            message.CreatedAt
        });
    }
}