namespace Tournamentz.BL.Commands
{
    using Core.Attribute;
    using Core.Command;
    using DAL.Entity;
    using System;

    public abstract class PlayerCommands
    {
        public class Create : CommandBase
        {
            public string Nickname { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }
        }

        public class Update : CommandBase
        {
            [ExistsInTable(typeof(Player))]
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }
        }

        public class Delete : CommandBase
        {
            [ExistsInTable(typeof(Player))]
            public Guid Id { get; set; }
        }
    }
}