namespace Tournamentz.BL.Core.Command
{
    using Rule;
    using System;
    using System.Linq;
    using Interfaces;

    public class CommandResult : ICommandResult
    {
        private Exception _exception;

        public CommandResult()
        {
            this.BusinessRules = new BusinessRuleCollection();
            this.PermissionRules = new BusinessRuleCollection();
        }

        public BusinessRuleCollection BusinessRules { get; set; }

        public BusinessRuleCollection PermissionRules { get; set; }

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
                if (this.PermissionRules.Any(r => r.IsBroken)) { return CommandResultStatus.PermissionError; }
                if (this.BusinessRules.Any(r => r.IsBroken)) { return CommandResultStatus.BrokenRules; }
                return CommandResultStatus.Success;
            }
        }

        public object ReturnValue { get; set; }

        public override string ToString()
        {
            switch (this.Status)
            {
                case CommandResultStatus.Success:
                    return string.Format("Success; return value {0}", this.ReturnValue ?? "null");

                case CommandResultStatus.PermissionError:
                    return string.Format("Permission error; count = {0}", this.PermissionRules.Count(p => p.IsBroken));

                case CommandResultStatus.BrokenRules:
                    return string.Format("Broken rules; count = {0}", this.BusinessRules.Count(b => b.IsBroken));

                case CommandResultStatus.SystemError:
                    return string.Format("System error; {0}", this.Exception);

                default:
                    return "Invalid result state";
            }
        }
    }
}