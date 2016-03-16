namespace Tournamentz.BL.Validators
{
    using Commands;
    using Core.Rule;
    using Core.Validation;

    public abstract class PlayerValidators
    {
        public class UsernameValidation
            : IValidator<PlayerCommands.Create>
            , IValidator<PlayerCommands.Update>
        {
            public BusinessRuleCollection Validate(PlayerCommands.Create command)
            {
                BusinessRuleCollection rules = new BusinessRuleCollection();

                if (command.Nickname.Length <= 3)
                {
                    rules.Add(new BusinessRule(true, "Username mora biti duži od 3 znaka"));
                }

                return rules;
            }

            public BusinessRuleCollection Validate(PlayerCommands.Update command)
            {
                return new BusinessRuleCollection();
            }
        }
    }
}