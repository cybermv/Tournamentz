namespace Tournamentz.BL.Core.Query
{
    using Autofac;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BasicQueryGate<TQuery> : IQueryGate<TQuery>
        where TQuery : IQuery
    {
        public IQueryResult<TQuery> Query(IExecutionContext context)
        {
            QueryResult<TQuery> result = new QueryResult<TQuery>();

            // 1. execute handler
            IQueryHandler<TQuery> handler = context.Services
                .Resolve<IQueryHandler<TQuery>>();

            try
            {
                result.Query = handler.Query(context);
            }
            catch (Exception ex)
            {
                // TODO: log exception
                result.Exception = ex;
                return result;
            }

            // TODO: log success/broken rules
            return result;
        }

        public IQueryResult<TQuery> Create(IExecutionContext context)
        {
            QueryResult<TQuery> result = new QueryResult<TQuery>();

            // 1. execute handler
            IQueryHandler<TQuery> handler = context.Services
                .Resolve<IQueryHandler<TQuery>>();

            try
            {
                TQuery entry = handler.Create(context);
                result.Query = new List<TQuery> { entry }.AsQueryable();
            }
            catch (Exception ex)
            {
                // TODO: log exception
                result.Exception = ex;
                return result;
            }

            // TODO: log success/broken rules
            return result;
        }
    }
}