namespace Tournamentz.BL.Validators
{
    using System;
    using System.Linq;
    using Commands;
    using Core;
    using Core.Rule;
    using Core.Validation;
    using DAL.Core;
    using DAL.Entity;

    public abstract class TournamentValidators
    {
        public class NameIsUniqueForPlayer : IValidator<TournamentCommands.Create>
        {
            public BusinessRuleCollection Validate(TournamentCommands.Create command)
            {
                IRepository<Tournament> tournamentsRepo = command.ExecutionContext.UnitOfWork.Repository<Tournament>();

                bool playerHasSameNameTournament = tournamentsRepo.Query
                    .Any(t => t.Title == command.Title &&
                              t.OrganizerId == command.ExecutionContext.User.Id);

                return new BusinessRule(
                    !playerHasSameNameTournament,
                    "Već postoji istoimeni turnir");
            }
        }

        public class OnlyOrganiserCanEditTournament
        {
            private BusinessRule ValidateInternal(IExecutionContext context, Guid tournamentId)
            {
                Tournament tournament = context.UnitOfWork.Repository<Tournament>().FindById(tournamentId);

                bool canEditTournament = context.User.Id == tournament.OrganizerId ||
                                         context.User.Roles.Any(r => r.RoleId == TournamentzRoles.AdminGuid);

                return new BusinessRule(
                    canEditTournament,
                    "Nemaš pravo mijenjati turnir jer nisi organizator");
            }
        }
    }
}
