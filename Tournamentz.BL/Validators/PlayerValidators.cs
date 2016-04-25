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
        public class UniqueUsernameValidation
            : IValidator<PlayerCommands.Create>
        {
            public BusinessRuleCollection Validate(PlayerCommands.Create command)
            {
                IRepository<Player> playersRepo = command.ExecutionContext.UnitOfWork.Repository<Player>();

                bool existsSameNamePlayer = playersRepo.Query
                    .Any(p => p.Nickname == command.Nickname);

                return new BusinessRule(
                    !existsSameNamePlayer,
                    "Nadimak igrača već postoji u sustavu!");
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