namespace Tournamentz.BL.Core.Validation
{
    using Command;
    using Rule;

    public interface IValidator
    {
        BusinessRuleCollection Validate(ICommand command);
    }

    public interface IValidator<TCommand> : IValidator
        where TCommand : ICommand
    {
        BusinessRuleCollection Validate(TCommand command);
    }
}