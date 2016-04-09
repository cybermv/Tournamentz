namespace Tournamentz.BL.CommandHandlers
{
    using Commands;
    using Core.Command;
    using Core.Command.Interface;
    using DAL.Core;
    using DAL.Entity;
    using System.Linq;

    public class PlayerCommandHandler : CommandHandlerBase
        , ICommandHandler<PlayerCommands.Create>
        , ICommandHandler<PlayerCommands.CreateOrRetrieve>
        , ICommandHandler<PlayerCommands.Update>
        , ICommandHandler<PlayerCommands.Delete>
    {
        public void Handle(PlayerCommands.Create command)
        {
            IRepository<Player> playerRepo = command.ExecutionContext.UnitOfWork.Repository<Player>();

            Player player = new Player
            {
                Nickname = command.Nickname,
                Name = command.Name,
                Surname = command.Surname
            };

            playerRepo.Insert(player);

            this.Result.ReturnValue = player.Id;
        }

        public void Handle(PlayerCommands.CreateOrRetrieve command)
        {
            IRepository<Player> playerRepo = command.ExecutionContext.UnitOfWork.Repository<Player>();

            Player existingPlayer = playerRepo.Query
                .SingleOrDefault(p => p.Nickname == command.Nickname);

            if (existingPlayer == null)
            {
                PlayerCommands.Create createCommand = new PlayerCommands.Create
                {
                    Nickname = command.Nickname,
                    ExecutionContext = command.ExecutionContext
                };

                object createdPlayerId = this.RunCommand(createCommand);
                this.Result.ReturnValue = createdPlayerId;
            }
            else
            {
                this.Result.ReturnValue = existingPlayer.Id;
            }
        }

        public void Handle(PlayerCommands.Update command)
        {
            IRepository<Player> playerRepo = command.ExecutionContext.UnitOfWork.Repository<Player>();

            Player player = playerRepo.FindById(command.Id);
            player.Name = command.Name;
            player.Surname = command.Surname;

            playerRepo.Update(player);
        }

        public void Handle(PlayerCommands.Delete command)
        {
            IRepository<Player> playerRepo = command.ExecutionContext.UnitOfWork.Repository<Player>();

            playerRepo.Delete(command.Id);
        }
    }
}