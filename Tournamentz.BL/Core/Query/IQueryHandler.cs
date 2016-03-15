namespace Tournamentz.BL.Core.Query
{
    using System.Linq;

    public interface IQueryHandler<TQuery>
        where TQuery : IQuery
    {
        IQueryable<TQuery> Query(IExecutionContext context);

        TQuery Create(IExecutionContext context);
    }
}