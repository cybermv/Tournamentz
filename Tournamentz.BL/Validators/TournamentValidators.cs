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
            : IValidator<TournamentCommands.AddTeam>
            , IValidator<TournamentCommands.RemoveTeam>
        {
            private static BusinessRule ValidateInternal(IExecutionContext context, Guid tournamentId)
            {
                Tournament tournament = context.UnitOfWork.Repository<Tournament>().FindById(tournamentId);

                bool canEditTournament = context.User.Id == tournament.OrganizerId ||
                                         context.User.Roles.Any(r => r.RoleId == TournamentzRoles.AdminGuid);

                return new BusinessRule(
                    canEditTournament,
                    "Nemaš pravo mijenjati turnir jer nisi organizator");
            }

            public BusinessRuleCollection Validate(TournamentCommands.AddTeam command)
            {
                return ValidateInternal(command.ExecutionContext, command.TournamentId);
            }

            public BusinessRuleCollection Validate(TournamentCommands.RemoveTeam command)
            {
                return ValidateInternal(command.ExecutionContext, command.TournamentId);
            }
        }

        public class TeamUniqueOnTournament
            : IValidator<TournamentCommands.AddTeam>
        {
            public BusinessRuleCollection Validate(TournamentCommands.AddTeam command)
            {
                IRepository<TournamentTeam> tournamentTeamsRepo =
                    command.ExecutionContext.UnitOfWork.Repository<TournamentTeam>();

                bool teamExistsOnTournament = tournamentTeamsRepo.Query
                    .Any(tt => tt.TeamId == command.TeamId &&
                               tt.TournamentId == command.TournamentId);

                return new BusinessRule(
                    !teamExistsOnTournament,
                    "Odabrani tim je već dodan na turnir");
            }
        }
    }
}
