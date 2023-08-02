namespace ToDoListManagement.Application.Abstraction
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery command);
    }
}
