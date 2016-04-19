namespace Tournamentz.BL.CommandHandlers
{
    using Commands;
    using Core.Command;
    using Core.Command.Interface;
    using DAL.Core;
    using DAL.Entity;

    public class TournamentCommandHandler : CommandHandlerBase
        , ICommandHandler<TournamentCommands.Create>
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
            
            // TODO: dodaj logiranog igraca kao sudionika turnira
        }
    }
}
