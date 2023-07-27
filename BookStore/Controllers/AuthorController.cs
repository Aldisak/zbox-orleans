using BookStore.Entities;
using BookStore.Grains;
using BookStore.States;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IGrainFactory _grainFactory;

    public AuthorController(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    [HttpGet("{surname}")]
    public async Task<IActionResult> Get([FromQuery] string surname)
    {
        var authorGrain = _grainFactory.GetGrain<IAuthorGrain>(surname);
        var fullName    = await authorGrain.GetAuthorAsync();
        return Ok(fullName);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Author author)
    {
        var authorGrain = _grainFactory.GetGrain<IAuthorGrain>(author.FullName.Surname);
        await authorGrain.SetAuthorAsync(author.FullName, author.DateBirth);
        return Ok("Surname updated successfully!");
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FullName fullName)
    {
        var authorGrain = _grainFactory.GetGrain<IAuthorGrain>(fullName.Surname);
        await authorGrain.CreateAuthorAsync(fullName, DateTime.Now);
        return Ok("Author created successfully!");
    }
}