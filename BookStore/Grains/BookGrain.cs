using BookStore.Entities;

namespace BookStore.Grains;

//POCO Grain: Vytvořte jednoduchý POCO grain, který neuchovává stav mezi voláními. POCO grainy v Orleans 7 nevyžadují dědění od třídy Grain.
//Stavový grain (bez persistence): Vytvořte stavový grain, který udržuje stav mezi voláními, ale není persistován.
public sealed class BookGrain : IBookGrain
{
    private Book? _book;

    public Task<Book?> GetBook() => Task.FromResult(_book);
    public Task SetBook(Book book) => Task.CompletedTask.ContinueWith(_ => _book = book);
}

public interface IBookGrain : IGrainWithIntegerKey
{
    Task<Book?> GetBook();
    Task SetBook(Book book);
}