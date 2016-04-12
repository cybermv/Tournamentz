namespace Tournamentz.BL.CommandHandlers
{
    using Commands;
    using Core.Command;
    using Core.Command.Interface;
    using DAL.Core;
    using DAL.Entity;

    public class TeamPlayerCommandHandler : CommandHandlerBase
        , ICommandHandler<TeamPlayerCommands.Create>
        , ICommandHandler<TeamPlayerCommands.Delete>
    {
        public void Handle(TeamPlayerCommands.Create command)
        {
            IRepository<TeamPlayer> teamPlayersRepo = command.ExecutionContext.UnitOfWork.Repository<TeamPlayer>();

            TeamPlayer newTeamPlayer = new TeamPlayer
            {
                TeamId = command.TeamId,
                PlayerId = command.PlayerId
            };

            teamPlayersRepo.Insert(newTeamPlayer);

            this.Result.ReturnValue = newTeamPlayer.Id;
        }

        public void Handle(TeamPlayerCommands.Delete command)
        {
            IRepository<TeamPlayer> teamPlayersRepo = command.ExecutionContext.UnitOfWork.Repository<TeamPlayer>();
            teamPlayersRepo.Delete(command.Id);
        }
    }
}
