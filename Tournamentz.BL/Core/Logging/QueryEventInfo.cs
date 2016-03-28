namespace Tournamentz.BL.Core.Logging
{
    using NLog;
    using Query.Interface;
    using System;

    public sealed class QueryEventInfo : LogEventInfo
    {
        public QueryEventInfo(IExecutionContext context, IQueryResult result)
        {
            this.Level = LogLevel.Info;
            this.Parameters = new object[] { result };
            this.Message = "Query execution - {0}";
            this.LoggerName = GetFriendlyQueryName(result);
        }

        private static string GetFriendlyQueryName(IQueryResult result)
        {
            Type queryType = result.Query.GetType().GetGenericArguments()[0];

            return queryType.DeclaringType != null
                ? $"{queryType.DeclaringType.Name}.{queryType.Name}"
                : queryType.Name;
        }
    }
}