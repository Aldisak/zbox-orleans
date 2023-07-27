using BankTransactions.Interfaces;
using Orleans.Concurrency;

namespace BankTransactions.Grains;

// Bezstavový grain (stateless): Vytvořte bezstavový grain, který má maximální propustnost v možnostech volání metod grainu.
[StatelessWorker]
public class AtmGrain : Grain, IAtmGrain
{
    public Task Transfer(
        string fromId,
        string toId,
        decimal amount) =>
        Task.WhenAll(
            GrainFactory.GetGrain<IAccountGrain>(fromId).Withdraw(amount),
            GrainFactory.GetGrain<IAccountGrain>(toId).Deposit(amount));
}