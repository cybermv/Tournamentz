namespace Tournamentz.BL.Core.Command.Interfaces
{
    using System;
    using Rule;

    public interface ICommandResult
    {
        BusinessRuleCollection BusinessRules { get; set; }

        BusinessRuleCollection PermissionRules { get; }

        Exception Exception { get; set; }

        CommandResultStatus Status { get; }

        object ReturnValue { get; set; }
    }
}