namespace Tournamentz.BL.Core.Validation
{
    using Command;
    using Command.Interfaces;
    using Rule;

    public interface IValidator<TCommand>
        where TCommand : ICommand
    {
        BusinessRuleCollection Validate(TCommand command);
    }
}