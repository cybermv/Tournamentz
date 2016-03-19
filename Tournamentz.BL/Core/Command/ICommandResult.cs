namespace Tournamentz.BL.Core.Command
{
    using Rule;
    using System;

    public interface ICommandResult
    {
        BusinessRuleCollection BusinessRules { get; set; }

        BusinessRuleCollection PermissionRules { get; }

        Exception Exception { get; set; }

        CommandResultStatus Status { get; }

        object ReturnValue { get; set; }
    }
}