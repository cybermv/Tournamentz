namespace Tournamentz.BL.Core.Command
{
    public interface ICommandGate
    {
        ICommandResult Run(ICommand command);
    }

    public interface ICommandGate<TCommand> : ICommandGate
        where TCommand : ICommand
    {
        ICommandResult Run(TCommand command);
    }
}