namespace Tournamentz.BL.CommandHandlers
{
    using Commands;
    using Core.Command;
    using Core.Command.Interface;
    using DAL.Core;
    using DAL.Entity;
    using System;
    using System.Linq;
    using Extensions;

    public class TeamCommandHandler : CommandHandlerBase
        , ICommandHandler<TeamCommands.Create>
        , ICommandHandler<TeamCommands.CreateOnePlayerTeam>
        , ICommandHandler<TeamCommands.AddExistingPlayer>
        , ICommandHandler<TeamCommands.AddNewPlayer>
        , ICommandHandler<TeamCommands.RemovePlayer>
        , ICommandHandler<TeamCommands.Rename>
        , ICommandHandler<TeamCommands.Delete>
    {
        public void Handle(TeamCommands.Create command)
        {
            IRepository<Team> teamRepo = command.ExecutionContext.UnitOfWork.Repository<Team>();

            Team newTeam = new Team
            {
                Title = command.Title,
                CreatorId = command.ExecutionContext.User.Id
            };

            teamRepo.Insert(newTeam);

            this.Result.ReturnValue = newTeam.Id;
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

            object playerId = this.RunCommand(createPlayer);

            if (this.CannotContinue) { return; }

            TeamCommands.Create createTeam = new TeamCommands.Create
            {
                Title = string.Format("Tim {0}", command.Nickname),
                ExecutionContext = command.ExecutionContext
            };

            object teamId = this.RunCommand(createTeam);

            if (this.CannotContinue) { return; }

            TeamCommands.AddExistingPlayer addExistingPlayer = new TeamCommands.AddExistingPlayer
            {
                TeamId = (Guid)teamId,
                Nickname = createPlayer.Nickname,
                ExecutionContext = command.ExecutionContext
            };

            object id = this.RunCommand(addExistingPlayer);

            if (this.CannotContinue) { return; }

            this.Result.ReturnValue = id;
        }

        public void Handle(TeamCommands.AddExistingPlayer command)
        {
            IRepository<TeamPlayer> teamPlayersRepo = command.ExecutionContext.UnitOfWork.Repository<TeamPlayer>();
            IRepository<Player> playerRepo = command.ExecutionContext.UnitOfWork.Repository<Player>();

            // TODO: check for null in validator
            Player player = playerRepo.FindByNickname(command.Nickname);

            TeamPlayer newTeamPlayer = new TeamPlayer
            {
                TeamId = command.TeamId,
                PlayerId = player.Id
            };

            teamPlayersRepo.Insert(newTeamPlayer);

            this.Result.ReturnValue = newTeamPlayer.Id;
        }

        public void Handle(TeamCommands.AddNewPlayer command)
        {
            PlayerCommands.Create createPlayer = new PlayerCommands.Create
            {
                Nickname = command.Nickname,
                Name = command.Name,
                Surname = command.Surname,
                ExecutionContext = command.ExecutionContext
            };

            object createdPlayerId = this.RunCommand(createPlayer);

            if (this.CannotContinue) { return; }

            TeamCommands.AddExistingPlayer addExistingPlayer = new TeamCommands.AddExistingPlayer
            {
                TeamId = command.TeamId,
                Nickname = command.Nickname,
                ExecutionContext = command.ExecutionContext
            };

            object createdTeamPlayerId = this.RunCommand(addExistingPlayer);

            if (this.CannotContinue) { return; }

            this.Result.ReturnValue = createdTeamPlayerId;
        }

        public void Handle(TeamCommands.RemovePlayer command)
        {
            IRepository<TeamPlayer> teamPlayersRepo = command.ExecutionContext.UnitOfWork.Repository<TeamPlayer>();

            TeamPlayer playerToRemove = teamPlayersRepo.Query
                .Single(tp => tp.TeamId == command.TeamId &&
                              tp.PlayerId == command.PlayerId);

            teamPlayersRepo.Delete(playerToRemove.Id);
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