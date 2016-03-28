namespace Tournamentz.BL.Core.Query.Interface
{
    using Rule;
    using System;
    using System.Linq;

    public interface IQueryResult
    {
        BusinessRuleCollection PermissionRules { get; }

        Exception Exception { get; }

        QueryResultStatus Status { get; }

        IQueryable Query { get; }
    }

    public interface IQueryResult<TQuery> : IQueryResult
        where TQuery : IQuery
    {
        new IQueryable<TQuery> Query { get; }
    }
}