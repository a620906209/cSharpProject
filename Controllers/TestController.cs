using Microsoft.AspNetCore.Mvc;
using SimpleApi.Data;
using SimpleApi.Repositories;

namespace SimpleApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IUserRepository _repository;

    public TestController(IUserRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _repository.GetAllAsync();
        return Ok(users);
    }
}