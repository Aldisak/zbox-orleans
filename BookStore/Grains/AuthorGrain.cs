using BookStore.Entities;
using BookStore.States;
using Orleans.Runtime;

namespace BookStore.Grains;

//Stavový grain (s persistencí): Implementujte stavový grain, který svůj stav persistuje. Můžete využít Azure CosmosDB nebo Azure Blob Storage pro persistenci stavu grainu.
public class AuthorGrain : Grain, IAuthorGrain
{
    private readonly IPersistentState<IAuthorState> _author;

    public AuthorGrain([PersistentState("author", "authorStore")] IPersistentState<IAuthorState> author)
    {
        _author = author;
    }

    public Task<Author> GetAuthorAsync() =>
        Task.FromResult(new Author(_author.State.FullName, _author.State.DateBirth));

    public Task SetAuthorAsync(FullName fullName, DateTime dateBirth)
    {
        _author.State.FullName  = fullName;
        _author.State.DateBirth = dateBirth;
        return _author.WriteStateAsync();
    }
    
    public Task CreateAuthorAsync(FullName fullName, DateTime dateBirth)
    {
        _author.State.FullName  = fullName;
        _author.State.DateBirth = dateBirth;
        return _author.WriteStateAsync();
    }
}
public interface IAuthorGrain : IGrainWithStringKey
{
    public Task<Author> GetAuthorAsync();
    public Task SetAuthorAsync(FullName fullName, DateTime dateBirth);
    public Task CreateAuthorAsync(FullName fullName, DateTime dateBirth);
}