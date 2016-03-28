namespace Tournamentz.BL.Core.Command.Interfaces
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        ICommandResult Result { get; }

        void Handle(TCommand command);
    }
}