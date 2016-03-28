namespace Tournamentz.BL.Core.Command.Interfaces
{
    public interface ICommand
    {
        IExecutionContext ExecutionContext { get; set; }
    }
}