namespace Tournamentz.BL.Validators
{
    using Commands;
    using Core.Rule;
    using Core.Validation;
    using DAL.Core;
    using DAL.Entity;
    using System.Linq;

    public abstract class PlayerValidators
    {
        public class UsernameValidation
            : IValidator<PlayerCommands.Create>
        {
            public BusinessRuleCollection Validate(PlayerCommands.Create command)
            {
                bool usernameBad = string.IsNullOrWhiteSpace(command.Nickname) || command.Nickname.Length <= 3;

                return new BusinessRule(
                    !usernameBad,
                    "Nickname mora biti duži od 3 znaka");
            }
        }

        public class CannotDeleteUsedPlayer
            : IValidator<PlayerCommands.Delete>
        {
            public BusinessRuleCollection Validate(PlayerCommands.Delete command)
            {
                IRepository<TeamPlayer> teamPlayersRepo = command.ExecutionContext.UnitOfWork.Repository<TeamPlayer>();

                bool playerUsedOnTeam = teamPlayersRepo.Query
                    .Any(tp => tp.PlayerId == command.Id);

                return new BusinessRule(
                    !playerUsedOnTeam,
                    "Igrač se ne može obrisati ukoliko postoji na nekom timu");
            }
        }
    }
}