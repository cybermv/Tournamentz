namespace Tournamentz.BL.Core.Command
{
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        ICommandResult Result { get; }

        void Handle(TCommand command);
    }
}