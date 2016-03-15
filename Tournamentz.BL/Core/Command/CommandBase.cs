namespace Tournamentz.BL.Core.Command
{
    public abstract class CommandBase : ICommand
    {
        public IExecutionContext ExecutionContext { get; set; }
    }
}