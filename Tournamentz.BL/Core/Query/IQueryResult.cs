namespace Tournamentz.BL.Core.Query
{
    using System;
    using System.Linq;

    public interface IQueryResult<TQuery>
        where TQuery : IQuery
    {
        Exception Exception { get; }

        QueryResultStatus Status { get; }

        IQueryable<TQuery> Query { get; }
    }
}