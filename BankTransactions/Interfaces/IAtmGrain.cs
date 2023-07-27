namespace BankTransactions.Interfaces;

public interface IAtmGrain : IGrainWithIntegerKey
{
    [Transaction(TransactionOption.Create)]
    Task Transfer(string fromId, string toId, decimal amountToTransfer);
}