namespace BookStore.States;

[GenerateSerializer]
public class IAuthorState
{
    public IAuthorState(FullName fullName, DateTime dateBirth)
    {
        FullName  = fullName;
        DateBirth = dateBirth;
    }

    [Id(0)]
    public FullName FullName { get; set; }
    [Id(1)]
    public DateTime DateBirth { get; set; }
}

[GenerateSerializer]
public class FullName
{
    public FullName(string name, string surname)
    {
        Name    = name;
        Surname = surname;
    }

    [Id(0)]
    public string Name { get; set; }
    [Id(1)]
    public string Surname { get; set; }
}