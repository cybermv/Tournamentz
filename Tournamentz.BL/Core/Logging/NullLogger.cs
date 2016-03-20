namespace Tournamentz.BL.Core.Logging
{
    using Command;
    using Query;

    /// <summary>
    /// A logger that discards all log entries
    /// </summary>
    public class NullLogger : ILogger
    {
        public void LogQuery<TQuery>(IExecutionContext context, IQueryResult<TQuery> result) where TQuery : IQuery
        {
        }

        public void LogCommand<TCommand>(TCommand command, ICommandResult result) where TCommand : ICommand
        {
        }

        public void Log(string title, string message, LogSeverity severity)
        {
        }
    }
}