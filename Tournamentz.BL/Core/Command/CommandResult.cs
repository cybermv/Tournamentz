namespace Tournamentz.BL.Core.Command
{
    using Rule;
    using System;
    using System.Linq;

    public class CommandResult : ICommandResult
    {
        private Exception _exception;

        public CommandResult()
        {
            this.BusinessRules = new BusinessRuleCollection();
        }

        public BusinessRuleCollection BusinessRules { get; private set; }

        public Exception Exception
        {
            get { return this._exception; }
            set
            {
                if (this._exception != null)
                {
                    throw new InvalidOperationException("Cannot set the Exception property more than once");
                }
                this._exception = value;
            }
        }

        public CommandResultStatus Status
        {
            get
            {
                if (this.Exception != null) { return CommandResultStatus.SystemError; }
                if (this.BusinessRules.Any(r => r.IsBroken)) { return CommandResultStatus.BrokenRules; }
                return CommandResultStatus.Success;
            }
        }

        public object ReturnValue { get; set; }
    }
}