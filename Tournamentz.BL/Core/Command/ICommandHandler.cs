namespace Tournamentz.BL.Core.Command
{
    public interface ICommandHandler
    {
        object ReturnValue { get; set; }

        void Handle(ICommand command);
    }

    public interface ICommandHandler<TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}