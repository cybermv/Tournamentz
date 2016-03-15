namespace Tournamentz.BL.Core.Command
{
    using Rule;
    using System;

    public interface ICommandResult
    {
        BusinessRuleCollection BusinessRules { get; }

        Exception Exception { get; }

        CommandResultStatus Status { get; }

        object ReturnValue { get; }
    }
}