namespace Tournamentz.DAL.Identity
{
    using Core;
    using Entity;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    public class ApplicationUserManager : UserManager<ApplicationUser, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserManager(IUnitOfWork unitOfWork)
            : base(new ApplicationUserStore(unitOfWork.Context))
        {
            this._unitOfWork = unitOfWork;
        }

        public override Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            IRepository<Player> playerRepo = this._unitOfWork.Repository<Player>();

            Player existingPlayer = playerRepo.Query
                .Include(p => p.ApplicationUser)
                .SingleOrDefault(p => p.Nickname == user.UserName);

            if (existingPlayer == null)
            {
                Player newPlayer = new Player
                {
                    Name = "",
                    Surname = "",
                    Nickname = user.UserName
                };

                user.Id = playerRepo.Insert(newPlayer).Id;
            }
            else if (existingPlayer.ApplicationUser == null)
            {
                user.Id = existingPlayer.Id;
            }
            else
            {
                return Task.FromResult(new IdentityResult("Username already taken!"));
            }

            return base.CreateAsync(user);
        }

        // TODO: move this manager to BL
    }
}