using BookStore.States;

namespace BookStore.Entities;

[GenerateSerializer]
public sealed class Author
{
    public Author(FullName fullName, DateTime dateBirth)
    {
        FullName  = fullName;
        DateBirth = dateBirth;
    }

    [Id(0)]
    public FullName FullName { get; set; }
    [Id(1)]
    public DateTime DateBirth { get; set; }
}