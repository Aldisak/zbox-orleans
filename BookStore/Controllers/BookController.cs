using BookStore.Entities;
using BookStore.Grains;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IGrainFactory _grainFactory;

    public BookController(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        var bookGrain = _grainFactory.GetGrain<IBookGrain>(id);

        var book = await bookGrain.GetBook();
        if (book is null) return NotFound("Book not found!");

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] Book book)
    {
        var bookGrain = _grainFactory.GetGrain<IBookGrain>(book.Id);
        await bookGrain.SetBook(book);
        return Ok("Book created successfully!");
    }
}