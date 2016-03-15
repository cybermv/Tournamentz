namespace Tournamentz.BL.Core.Command
{
    public interface ICommand
    {
        IExecutionContext ExecutionContext { get; set; }
    }
}