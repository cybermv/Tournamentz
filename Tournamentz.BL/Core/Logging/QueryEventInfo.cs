namespace Tournamentz.BL.Core.Logging
{
    using NLog;
    using Query.Interface;
    using System;

    public sealed class QueryEventInfo<TQuery> : LogEventInfo
        where TQuery : IQuery
    {
        public QueryEventInfo(IExecutionContext context, IQueryResult result)
        {
            this.Level = LogLevel.Info;
            this.Parameters = new object[] { result };
            this.Message = "Query execution - {0}";
            this.LoggerName = GetFriendlyQueryName();
        }

        private static string GetFriendlyQueryName()
        {
            Type queryType = typeof(TQuery);

            return queryType.DeclaringType != null
                ? $"{queryType.DeclaringType.Name}.{queryType.Name}"
                : queryType.Name;
        }
    }
}