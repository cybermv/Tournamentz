namespace Tournamentz.BL.Commands
{
    using Core.Attribute;
    using Core.Command;
    using DAL.Entity;
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class PlayerCommands
    {
        public class Create : CommandBase
        {
            [Display(Name = "Nadimak")]
            public string Nickname { get; set; }

            [Display(Name = "Ime")]
            public string Name { get; set; }

            [Display(Name = "Prezime")]
            public string Surname { get; set; }
        }

        public class CreateOrRetrieve : CommandBase
        {
            [Display(Name = "Nadimak")]
            public string Nickname { get; set; }
        }

        public class Update : CommandBase
        {
            [ExistsInTable(typeof(Player))]
            public Guid Id { get; set; }

            [Display(Name = "Ime")]
            public string Name { get; set; }

            [Display(Name = "Prezime")]
            public string Surname { get; set; }
        }

        public class Delete : CommandBase
        {
            [ExistsInTable(typeof(Player))]
            public Guid Id { get; set; }
        }
    }
}