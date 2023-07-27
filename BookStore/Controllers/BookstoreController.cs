using BookStore.Entities;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace BookStore.Controllers;

[ApiController]
[Route("[controller]")]
public class BookstoreController : ControllerBase
{
    private readonly IGrainFactory _grainFactory;
    
    public BookstoreController(IGrainFactory grainFactory)
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

public interface IBookGrain : IGrainWithIntegerKey
{
    Task<Book?> GetBook();
    Task SetBook(Book book);
}

public sealed class BookGrain : IBookGrain
{
    private Book? _book;
    
    public Task<Book?> GetBook() => Task.FromResult(_book);
    public Task SetBook(Book book) => Task.CompletedTask.ContinueWith(_ => _book = book);
}