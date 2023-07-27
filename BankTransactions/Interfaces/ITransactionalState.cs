namespace BankTransactions.Interfaces;

public interface ITransactionalState<TState>
    where TState : class, new()
{
    Task<TResult> PerformRead<TResult>(
        Func<TState, TResult> readFunction);

    Task<TResult> PerformUpdate<TResult>(
        Func<TState, TResult> updateFunction);
}