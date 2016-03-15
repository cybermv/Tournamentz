namespace Tournamentz.BL.CommandHandlers
{
    using Commands;
    using Core.Command;
    using DAL.Core;
    using DAL.Entity;

    public class PlayerCommandHandler : CommandHandlerBase
        , ICommandHandler<PlayerCommands.Create>
        , ICommandHandler<PlayerCommands.Update>
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

        public void Handle(PlayerCommands.Update command)
        {
            IRepository<Player> playerRepo = command.ExecutionContext.UnitOfWork.Repository<Player>();

            Player player = playerRepo.FindById(command.Id);
            player.Name = command.Name;
            player.Surname = command.Surname;

            playerRepo.Update(player);
        }
    }
}