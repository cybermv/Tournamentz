namespace Tournamentz.BL.Commands
{
    using System;
    using Core.Attribute;
    using Core.Command;
    using DAL.Entity;

    public abstract class TeamCommands
    {
        [RequiresRole(TournamentzRoles.User)]
        public class Create : CommandBase
        {
            public string Title { get; set; }
        }

        [RequiresRole(TournamentzRoles.User)]
        public class CreateOnePlayerTeam : CommandBase
        {
            public string Nickname { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }
        }

        [RequiresRole(TournamentzRoles.User)]
        public class AddExistingPlayer : CommandBase
        {
            [ExistsInTable(typeof(Team))]
            public Guid TeamId { get; set; }

            [ExistsInTable(typeof(Player))]
            public Guid PlayerId { get; set; }
        }

        [RequiresRole(TournamentzRoles.User)]
        public class AddNewPlayer : CommandBase
        {
            [ExistsInTable(typeof(Team))]
            public Guid TeamId { get; set; }

            public string Nickname { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }
        }

        [RequiresRole(TournamentzRoles.User)]
        public class RemovePlayer : CommandBase
        {
            [ExistsInTable(typeof(Team))]
            public Guid TeamId { get; set; }

            [ExistsInTable(typeof(Player))]
            public Guid PlayerId { get; set; }
        }

        [RequiresRole(TournamentzRoles.Admin)]
        public class Rename : CommandBase
        {
            [ExistsInTable(typeof(Team))]
            public Guid Id { get; set; }

            public string Title { get; set; }
        }

        [RequiresRole(TournamentzRoles.Admin)]
        public class Delete : CommandBase
        {
            [ExistsInTable(typeof(Team))]
            public Guid Id { get; set; }
        }
    }
}
