namespace Tournamentz.BL.Core.Logging
{
    using Command;
    using NLog;
    using System;

    public sealed class CommandEventInfo<TCommand> : LogEventInfo
        where TCommand : ICommand
    {
        public CommandEventInfo(TCommand command, ICommandResult result)
        {
            this.Level = LogLevel.Info;
            this.Parameters = new object[] { command, result };
            this.Message = "Command execution - {0}";
            this.LoggerName = GetFriendlyCommandName();
        }

        private static string GetFriendlyCommandName()
        {
            Type commandType = typeof(TCommand);

            return commandType.DeclaringType != null
                ? $"{commandType.DeclaringType.Name}.{commandType.Name}"
                : commandType.Name;
        }
    }
}