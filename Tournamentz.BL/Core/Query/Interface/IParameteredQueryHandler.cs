namespace Tournamentz.BL.Core.Query.Interface
{
    using System.Linq;

    public interface IParameteredQueryHandler<TQuery, TParam>
        where TQuery : IQuery
    {
        IQueryable<TQuery> Query(IExecutionContext context, TParam parameter);
    }
}