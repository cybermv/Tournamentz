namespace Tournamentz.BL.Validators
{
    using System.Linq;
    using Commands;
    using Core.Rule;
    using Core.Validation;
    using DAL.Core;
    using DAL.Entity;
    using Extensions;

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

        public class PlayerMustExist
            : IValidator<TeamCommands.AddExistingPlayer>
        {
            public BusinessRuleCollection Validate(TeamCommands.AddExistingPlayer command)
            {
                IRepository<Player> playerRepo = command.ExecutionContext.UnitOfWork.Repository<Player>();

                Player player = playerRepo.FindByNickname(command.Nickname);

                return new BusinessRule(
                    player != null,
                    "Igrač sa navedenim nadimkom nije pronađen u sustavu");
            }
        }

        public class PlayerCannotBeOnTeamTwice
            : IValidator<TeamCommands.AddExistingPlayer>
        {
            public BusinessRuleCollection Validate(TeamCommands.AddExistingPlayer command)
            {
                IRepository<TeamPlayer> teamPlayerRepo = command.ExecutionContext.UnitOfWork.Repository<TeamPlayer>();

                bool playerExistsOnTeam = teamPlayerRepo.Query
                    .Any(tp => tp.TeamId == command.TeamId &&
                               tp.Player.Nickname == command.Nickname);

                return new BusinessRule(
                    !playerExistsOnTeam,
                    "Igrač je već član tima");
            }
        }
    }
}
