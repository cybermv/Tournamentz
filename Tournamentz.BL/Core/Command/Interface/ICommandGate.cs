namespace Tournamentz.BL.Core.Command.Interface
{
    public interface ICommandGate<TCommand>
        where TCommand : ICommand
    {
        ICommandResult Run(TCommand command);
    }
}