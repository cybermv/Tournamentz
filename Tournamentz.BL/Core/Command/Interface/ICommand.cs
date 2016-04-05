namespace Tournamentz.BL.Core.Command.Interface
{
    public interface ICommand
    {
        IExecutionContext ExecutionContext { get; set; }
    }
}