namespace Tournamentz.BL.Commands
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Core.Attribute;
    using Core.Command;
    using DAL.Entity;

    public abstract class TeamCommands
    {
        [RequiresRole(TournamentzRoles.User)]
        public class Create : CommandBase
        {
            [Display(Name = "Naziv")]
            public string Title { get; set; }
        }

        [RequiresRole(TournamentzRoles.User)]
        public class CreateOnePlayerTeam : CommandBase
        {
            [Display(Name = "Nadimak")]
            public string Nickname { get; set; }

            [Display(Name = "Ime")]
            public string Name { get; set; }

            [Display(Name = "Prezime")]
            public string Surname { get; set; }
        }

        [RequiresRole(TournamentzRoles.User)]
        public class AddExistingPlayer : CommandBase
        {
            [ExistsInTable(typeof(Team))]
            public Guid TeamId { get; set; }

            [Display(Name = "Nadimak")]
            public string Nickname { get; set; }
        }

        [RequiresRole(TournamentzRoles.User)]
        public class AddNewPlayer : CommandBase
        {
            [ExistsInTable(typeof(Team))]
            public Guid TeamId { get; set; }

            [Display(Name = "Nadimak")]
            public string Nickname { get; set; }

            [Display(Name = "Ime")]
            public string Name { get; set; }

            [Display(Name = "Prezime")]
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

            [Display(Name = "Naziv")]
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
