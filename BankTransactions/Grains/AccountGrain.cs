using BankTransactions.Entities;
using BankTransactions.Interfaces;
using Orleans.Concurrency;
using Orleans.Transactions.Abstractions;

namespace BankTransactions.Grains;

// Distributed Transactions: Vytvořte přiklad grainů, které ukazují, jak v Orleans provádět distribuované transakce. Mohlo by to být například jednoduché bankovní rozhraní, kde můžete převádět peníze mezi účty a musíte se ujistit, že transakce jsou konzistentní.
// Reentrant Grains: Vytvořte grain, který demonstruje použití vlatnosti reentrant v Orleans.
[Reentrant]
public class AccountGrain : Grain, IAccountGrain
{
    private readonly Interfaces.ITransactionalState<Balance> _balance;

    public AccountGrain(
        [TransactionalState(nameof(balance))] Interfaces.ITransactionalState<Balance> balance) =>
        _balance = balance ?? throw new ArgumentNullException(nameof(balance));

    public Task Deposit(decimal amount) =>
        _balance.PerformUpdate(
            balance => balance.Value += amount);

    public Task Withdraw(decimal amount) =>
        _balance.PerformUpdate(balance =>
        {
            if (balance.Value < amount)
            {
                throw new InvalidOperationException(
                    $"Withdrawing {amount} credits from account "          +
                    $"\"{this.GetPrimaryKeyString()}\" would overdraw it." +
                    $" This account has {balance.Value} credits.");
            }

            return balance.Value -= amount;
        });

    public Task<decimal> GetBalance() =>
        _balance.PerformRead(balance => balance.Value);
}