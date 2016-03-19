namespace Tournamentz.BL.Core.Query
{
    using Rule;
    using System;
    using System.Linq;

    public interface IQueryResult<TQuery>
        where TQuery : IQuery
    {
        BusinessRuleCollection PermissionRules { get; }

        Exception Exception { get; }

        QueryResultStatus Status { get; }

        IQueryable<TQuery> Query { get; }
    }
}