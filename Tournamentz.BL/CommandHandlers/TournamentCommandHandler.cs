namespace Tournamentz.BL.CommandHandlers
{
    using System.Linq;
    using Commands;
    using Core.Command;
    using Core.Command.Interface;
    using DAL.Core;
    using DAL.Entity;

    public class TournamentCommandHandler : CommandHandlerBase
        , ICommandHandler<TournamentCommands.Create>
        , ICommandHandler<TournamentCommands.AddTeam>
        , ICommandHandler<TournamentCommands.RemoveTeam>
    {
        public void Handle(TournamentCommands.Create command)
        {
            IRepository<Tournament> tournamentsRepo = command.ExecutionContext.UnitOfWork.Repository<Tournament>();

            Tournament newTournament = new Tournament
            {
                Title = command.Title,
                OrganizerId = command.ExecutionContext.User.Id,
                TournamentTypeId = command.TournamentTypeId
            };

            tournamentsRepo.Insert(newTournament);

            this.Result.ReturnValue = newTournament.Id;
        }

        public void Handle(TournamentCommands.AddTeam command)
        {
            IRepository<TournamentTeam> tournamentTeamsRepo =
                    command.ExecutionContext.UnitOfWork.Repository<TournamentTeam>();

            TournamentTeam newTournamentTeam = new TournamentTeam
            {
                TournamentId = command.TournamentId,
                TeamId = command.TeamId
            };

            tournamentTeamsRepo.Insert(newTournamentTeam);

            this.Result.ReturnValue = newTournamentTeam.Id;
        }

        public void Handle(TournamentCommands.RemoveTeam command)
        {
            IRepository<TournamentTeam> tournamentTeamsRepo =
                command.ExecutionContext.UnitOfWork.Repository<TournamentTeam>();

            TournamentTeam teamToRemove = tournamentTeamsRepo.Query
                .Single(tt => tt.TournamentId == command.TournamentId &&
                              tt.TeamId == command.TeamId);

            tournamentTeamsRepo.Delete(teamToRemove.Id);
        }
    }
}
