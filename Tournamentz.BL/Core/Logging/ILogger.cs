namespace Tournamentz.BL.Core.Logging
{
    using Command.Interfaces;
    using Query.Interface;

    public interface ILogger
    {
        void LogQuery<TQuery>(IExecutionContext context, IQueryResult result)
            where TQuery : IQuery;

        void LogCommand<TCommand>(TCommand command, ICommandResult result)
            where TCommand : ICommand;

        void Log(string title, string message, LogSeverity severity);
    }
}