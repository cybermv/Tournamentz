namespace Tournamentz.BL.Core.Query.Interface
{
    using System.Linq;

    public interface IQueryHandler<TQuery>
        where TQuery : IQuery
    {
        IQueryable<TQuery> Query(IExecutionContext context);
    }
}