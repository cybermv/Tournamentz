namespace Tournamentz.BL.Core.Logging
{
    using Command;
    using Query;

    public interface ILogger
    {
        void LogQuery<TQuery>(IExecutionContext context, IQueryResult<TQuery> result)
            where TQuery : IQuery;

        void LogCommand<TCommand>(TCommand command, ICommandResult result)
            where TCommand : ICommand;

        void Log(string title, string message, LogSeverity severity);
    }
}