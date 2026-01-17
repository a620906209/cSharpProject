using Microsoft.AspNetCore.Mvc;
using SimpleApi.Dtos.Requests;
using SimpleApi.Data;
using SimpleApi.Services;

namespace SimpleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _service.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}/id")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _service.GetByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequestDto request)
    {
        var user = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpGet("{id:int}/messages")]
    public async Task<IActionResult> GetMessagesByUser(int id)
    {
        var userExists = await _service.GetByIdAsync(id);
        if (userExists is null)
        {
            return NotFound(new { message = $"找不到 Id = {id} 的使用者" });
        }

        var messages = await _service.GetMessagesByUserAsync(id);

        return Ok(messages.Select(m => new
        {
            m.Id,
            m.Content,
            m.CreatedAt
        }));
    }

    [HttpGet("{id:int}/with-messages")]
    public async Task<IActionResult> GetUserWithMessages(int id)
    {
        var user = await _service.GetUserWithMessagesAsync(id);

        if (user is null)
        {
            return NotFound(new { message = $"找不到 Id = {id} 的使用者" });
        }

        return Ok(new
        {
            user.Id,
            user.Name,
            user.Phone,
            user.Birthday,
            user.UpdatedAt,
            Messages = user.HelloMessages
                .OrderByDescending(m => m.CreatedAt)
                .Select(m => new
                {
                    m.Id,
                    m.Content,
                    m.CreatedAt
                })
        });
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return BadRequest(new { message = "必須提供搜尋關鍵字" });
        }

        var users = await _service.SearchByNameAsync(term);
        return Ok(users);
    }
}
