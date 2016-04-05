namespace Tournamentz.BL.Core.Command
{
    using Interface;

    public abstract class CommandBase : ICommand
    {
        public IExecutionContext ExecutionContext { get; set; }
    }
}