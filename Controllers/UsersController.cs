using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Dtos.Requests;
using SimpleApi.Repositories;
using SimpleApi.Data;

namespace SimpleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly AppDbContext _context;

    public UsersController(IUserRepository repository, AppDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _repository.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id:int}/id")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequestDto request)
    {
        var user = new User
        {
            Name = request.Name,
            Phone = request.Phone,
            Birthday = request.Birthday ?? default,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpGet("{id:int}/messages")]
    public async Task<IActionResult> GetMessagesByUser(int id)
    {
        var userExists = await _repository.GetByIdAsync(id);
        if (userExists is null)
        {
            return NotFound(new { message = $"找不到 Id = {id} 的使用者" });
        }

        var messages = await _context.HelloMessages
            .Where(m => m.UserId == id)
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

    [HttpGet("{id:int}/with-messages")]
    public async Task<IActionResult> GetUserWithMessages(int id)
    {
        var user = await _context.Users
            .Include(u => u.HelloMessages)
            .FirstOrDefaultAsync(u => u.Id == id);

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

        var users = await _repository.SearchByNameAsync(term);
        return Ok(users);
    }
}
