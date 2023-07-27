using Orleans;

namespace BookStore.Entities;

[GenerateSerializer]
public sealed class Book
{
    public Book(int id, string title, string author)
    {
        Id     = id;
        Title  = title;
        Author = author;
    }

    [Id(0)]
    public int Id { get; set; }
    [Id(1)]
    public string Title { get; set; }
    [Id(2)]
    public string Author { get; set; }
}