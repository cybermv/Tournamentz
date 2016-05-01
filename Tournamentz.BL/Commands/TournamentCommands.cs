namespace Tournamentz.BL.Commands
{
    using System;
    using Core.Attribute;
    using Core.Command;
    using DAL.Entity;

    public abstract class TournamentCommands
    {
        [RequiresRole(TournamentzRoles.User)]
        public class Create : CommandBase
        {
            public string Title { get; set; }

            [ExistsInTable(typeof(TournamentType))]
            public Guid TournamentTypeId { get; set; }
        }

        [RequiresRole(TournamentzRoles.User)]
        public class AddTeam : CommandBase
        {
            [ExistsInTable(typeof(Tournament))]
            public Guid TournamentId { get; set; }

            [ExistsInTable(typeof(Team))]
            public Guid TeamId { get; set; }
        }

        [RequiresRole(TournamentzRoles.User)]
        public class RemoveTeam : CommandBase
        {
            [ExistsInTable(typeof(Tournament))]
            public Guid TournamentId { get; set; }

            [ExistsInTable(typeof(Team))]
            public Guid TeamId { get; set; }
        }
    }
}
