namespace Tournamentz.BL.Core.Query.Interface
{
    public interface IParameteredQueryGate<TQuery, TParam>
        where TQuery : IQuery
    {
        IQueryResult<TQuery> Query(IExecutionContext context, TParam parameter);

        IQueryResult<TQuery> Create(IExecutionContext context);
    }
}