namespace Tournamentz.BL.Commands
{
    using System;
    using Core.Attribute;
    using Core.Command;
    using DAL.Entity;

    public abstract class TeamPlayerCommands
    {
        [RequiresRole(TournamentzRoles.User)]
        public class Create : CommandBase
        {
            [ExistsInTable(typeof(Team))]
            public Guid TeamId { get; set; }

            [ExistsInTable(typeof(Player))]
            public Guid PlayerId { get; set; }
        }

        [RequiresRole(TournamentzRoles.User)]
        public class Delete : CommandBase
        {
            [ExistsInTable(typeof(TeamPlayer))]
            public Guid Id { get; set; }
        }
    }
}
