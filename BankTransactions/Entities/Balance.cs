namespace BankTransactions.Entities;

[GenerateSerializer]
public record class Balance
{
    [Id(0)]
    public decimal Value { get; set; } = 1_000;
}