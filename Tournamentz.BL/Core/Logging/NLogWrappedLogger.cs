namespace Tournamentz.BL.Core.Logging
{
    using Autofac;
    using Command;
    using NLog;
    using NLog.Config;
    using Query.Interface;
    using System;

    public sealed class NLogWrappedLogger : ILogger
    {
        private readonly Logger _innerLogger;
        private readonly IContainer _services;

        public NLogWrappedLogger(IContainer container)
        {
            this._services = container;

            ConfigurationItemFactory.Default = new ConfigurationItemFactory(AppDomain.CurrentDomain.GetAssemblies());

            ConfigurationItemCreator itemCreatorBase = ConfigurationItemFactory.Default.CreateInstance;
            ConfigurationItemFactory.Default.CreateInstance = type =>
            {
                object resolved = this._services.ResolveOptional(type);
                if (resolved != null) { return resolved; }

                return itemCreatorBase(type);
            };

            this._innerLogger = LogManager.GetLogger("Common");
        }

        public void LogQuery<TQuery>(IExecutionContext context, IQueryResult result)
            where TQuery : IQuery
        {
            // TODO: write to log
            //this._innerLogger.Info("LogQuery - " + result);
            LogEventInfo eventInfo = new QueryEventInfo<TQuery>(context, result);
            this._innerLogger.Log(eventInfo);
        }

        public void LogCommand<TCommand>(TCommand command, ICommandResult result)
            where TCommand : ICommand
        {
            // TODO: write to log
            //this._innerLogger.Info("LogCommand - " + result);

            LogEventInfo eventInfo = new CommandEventInfo<TCommand>(command, result);
            this._innerLogger.Log(eventInfo);
        }

        public void Log(string title, string message, LogSeverity severity)
        {
            // TODO: write to log
            this._innerLogger.Log(LogLevel.FromString(severity.ToString()), $"{title} - {message}");
        }
    }
}