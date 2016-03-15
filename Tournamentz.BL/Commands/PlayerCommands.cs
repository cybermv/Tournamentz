namespace Tournamentz.BL.Commands
{
    using Core.Command;
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
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }
        }
    }
}