namespace Tournamentz.BL.Core.Command
{
    using Interfaces;

    public abstract class CommandBase : ICommand
    {
        public IExecutionContext ExecutionContext { get; set; }
    }
}