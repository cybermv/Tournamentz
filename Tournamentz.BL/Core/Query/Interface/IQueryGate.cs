namespace Tournamentz.BL.Core.Query.Interface
{
    public interface IQueryGate<TQuery>
        where TQuery : IQuery
    {
        IQueryResult<TQuery> Query(IExecutionContext context);

        IQueryResult<TQuery> Create(IExecutionContext context);
    }
}