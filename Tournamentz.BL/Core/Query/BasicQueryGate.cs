namespace Tournamentz.BL.Core.Query
{
    using Autofac;
    using Interface;
    using Logging;
    using Rule;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Validation;

    public class BasicQueryGate<TQuery> : IQueryGate<TQuery>
        where TQuery : IQuery
    {
        public IQueryResult<TQuery> Query(IExecutionContext context)
        {
            ILogger logger = context.Services.Resolve<ILogger>();
            QueryResult<TQuery> result = new QueryResult<TQuery>();

            // 1. validate RequiresRole attributes
            BusinessRuleCollection permissionRules = RoleValidator.ValidateAttributes<TQuery>(context);
            result.PermissionRules.Add(permissionRules);

            if (result.Status != QueryResultStatus.Success)
            {
                logger.LogQuery<TQuery>(context, result);
                return result;
            }

            // 2. execute handler
            IQueryHandler<TQuery> handler = context.Services
                .Resolve<IQueryHandler<TQuery>>();

            try
            {
                result.Query = handler.Query(context);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                logger.LogQuery<TQuery>(context, result);
                return result;
            }

            logger.LogQuery<TQuery>(context, result);
            return result;
        }

        public IQueryResult<TQuery> Create(IExecutionContext context)
        {
            ILogger logger = context.Services.Resolve<ILogger>();
            QueryResult<TQuery> result = new QueryResult<TQuery>();

            // 1. execute handler
            ICreateHandler<TQuery> handler = context.Services
                .Resolve<ICreateHandler<TQuery>>();

            try
            {
                TQuery entry = handler.Create(context);
                result.Query = new List<TQuery> { entry }.AsQueryable();
            }
            catch (Exception ex)
            {
                result.Exception = ex;
                logger.LogQuery<TQuery>(context, result);
                return result;
            }

            logger.LogQuery<TQuery>(context, result);
            return result;
        }
    }
}