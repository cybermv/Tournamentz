namespace Tournamentz.BL.Core.Command
{
    using Interface;
    using Rule;

    public abstract class CommandHandlerBase
    {
        protected CommandHandlerBase()
        {
            this.Result = new CommandResult();
        }

        public ICommandResult Result { get; private set; }

        public bool CannotContinue { get { return this.Result.Status != CommandResultStatus.Success; } }

        public void AddRule(BusinessRule rule)
        {
            this.Result.BusinessRules.Add(rule);
        }
    }
}