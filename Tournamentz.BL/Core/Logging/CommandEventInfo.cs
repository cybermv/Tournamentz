namespace Tournamentz.BL.Core.Logging
{
    using Command.Interfaces;
    using NLog;
    using System;

    public sealed class CommandEventInfo : LogEventInfo
    {
        public CommandEventInfo(ICommand command, ICommandResult result)
        {
            this.Level = LogLevel.Info;
            this.Parameters = new object[] { command, result };
            this.Message = "Command execution - {0}";
            this.LoggerName = GetFriendlyCommandName(command);
        }

        private static string GetFriendlyCommandName(ICommand command)
        {
            Type commandType = command.GetType();

            return commandType.DeclaringType != null
                ? $"{commandType.DeclaringType.Name}.{commandType.Name}"
                : commandType.Name;
        }
    }
}