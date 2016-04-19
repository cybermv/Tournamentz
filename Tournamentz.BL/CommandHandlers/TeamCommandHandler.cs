namespace Tournamentz.BL.CommandHandlers
{
    using System;
    using Commands;
    using Core.Command;
    using Core.Command.Interface;
    using DAL.Core;
    using DAL.Entity;

    public class TeamCommandHandler : CommandHandlerBase
        , ICommandHandler<TeamCommands.Create>
        , ICommandHandler<TeamCommands.CreateOnePlayerTeam>
        , ICommandHandler<TeamCommands.Rename>
        , ICommandHandler<TeamCommands.Delete>
    {
        public void Handle(TeamCommands.Create command)
        {
            IRepository<Team> teamRepo = command.ExecutionContext.UnitOfWork.Repository<Team>();

            Team newTeam = new Team
            {
                Title = command.Title
            };

            teamRepo.Insert(newTeam);

            this.Result.ReturnValue = newTeam;
        }

        public void Handle(TeamCommands.CreateOnePlayerTeam command)
        {
            PlayerCommands.Create createPlayer = new PlayerCommands.Create
            {
                Name = command.Name,
                Surname = command.Surname,
                Nickname = command.Nickname,
                ExecutionContext = command.ExecutionContext
            };

            Guid playerId = this.RunCommand<PlayerCommands.Create, Guid>(createPlayer);

            if (this.CannotContinue) { return; }

            TeamCommands.Create createTeam = new TeamCommands.Create
            {
                Title = string.Format("Team {0}", command.Nickname),
                ExecutionContext = command.ExecutionContext
            };

            Guid teamId = this.RunCommand<TeamCommands.Create, Guid>(createTeam);

            if (this.CannotContinue) { return; }

            TeamPlayerCommands.Create createTeamPlayer = new TeamPlayerCommands.Create
            {
                TeamId = teamId,
                PlayerId = playerId,
                ExecutionContext = command.ExecutionContext
            };

            Guid id = this.RunCommand<TeamPlayerCommands.Create, Guid>(createTeamPlayer);

            if (this.CannotContinue) { return; }

            this.Result.ReturnValue = id;
        }

        public void Handle(TeamCommands.Rename command)
        {
            IRepository<Team> teamRepo = command.ExecutionContext.UnitOfWork.Repository<Team>();

            Team team = teamRepo.FindById(command.Id);

            team.Title = command.Title;

            teamRepo.Update(team);
        }

        public void Handle(TeamCommands.Delete command)
        {
            IRepository<Team> teamRepo = command.ExecutionContext.UnitOfWork.Repository<Team>();
            teamRepo.Delete(command.Id);
        }
    }
}
