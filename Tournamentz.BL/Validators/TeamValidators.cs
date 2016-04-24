namespace Tournamentz.BL.Validators
{
    using System.Linq;
    using Commands;
    using Core.Rule;
    using Core.Validation;
    using DAL.Core;
    using DAL.Entity;

    public abstract class TeamValidators
    {
        public class TeamNameUnique
            : IValidator<TeamCommands.Create>
            , IValidator<TeamCommands.Rename>
        {
            public BusinessRuleCollection Validate(TeamCommands.Create command)
            {
                IRepository<Team> teamRepo = command.ExecutionContext.UnitOfWork.Repository<Team>();

                bool nameIsUsed = teamRepo
                    .Any(t => t.Title == command.Title);

                return new BusinessRule(
                    !nameIsUsed,
                    "Već postoji istoimeni tim u sustavu");
            }

            public BusinessRuleCollection Validate(TeamCommands.Rename command)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
