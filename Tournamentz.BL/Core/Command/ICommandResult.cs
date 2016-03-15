namespace Tournamentz.BL.Core.Command
{
    using Rule;
    using System;

    public interface ICommandResult
    {
        BusinessRuleCollection BusinessRules { get; set; }

        Exception Exception { get; set; }

        CommandResultStatus Status { get; }

        object ReturnValue { get; set; }
    }
}