namespace Tournamentz.DAL
{
    using Core;
    using Entity;
    using Identity;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserCreateDatabaseInitializer : IDatabaseInitializer<TournamentzModelContext>
    {
        public void InitializeDatabase(TournamentzModelContext context)
        {
            if (!context.Database.Exists())
            {
                context.Database.Create();
            }

            this.Seed(context);
            context.SaveChanges();
        }

        protected void Seed(TournamentzModelContext context)
        {
            if (context.Players.Any()) { return; }

            BasicUnitOfWork uow = new BasicUnitOfWork(context);

            Player adminPlayer = new Player { Nickname = "admin", Name = "System", Surname = "Admin" };
            Player player1 = new Player { Nickname = "prvi.user", Name = "Prvi", Surname = "User" };
            Player player2 = new Player { Nickname = "drugi.user", Name = "Drugi", Surname = "User" };
            Player player3 = new Player { Nickname = "treci.user", Name = "Treći", Surname = "User" };
            Player player4 = new Player { Nickname = "cetvrti.user", Name = "Četvrti", Surname = "User" };
            Player player5 = new Player { Nickname = "peti.user", Name = "Peti", Surname = "User" };

            IRepository<Player> playerRepo = uow.Repository<Player>();

            playerRepo.Insert(adminPlayer);
            playerRepo.Insert(player1);
            playerRepo.Insert(player2);
            playerRepo.Insert(player3);
            playerRepo.Insert(player4);
            playerRepo.Insert(player5);

            ApplicationRole adminRole = new ApplicationRole { Id = new Guid("817EF2BA-3863-4C3A-954E-CBBA8809F361"), Name = "Admin" };
            ApplicationRole userRole = new ApplicationRole { Id = new Guid("3E2DDD01-0243-4E38-B563-80BCA9EF7BA7"), Name = "User" };

            IRepository<ApplicationRole> roleRepo = uow.Repository<ApplicationRole>();

            roleRepo.Insert(adminRole);
            roleRepo.Insert(userRole);

            ApplicationUserManager userManager = new ApplicationUserManager(uow);

            ApplicationUser adminUser = new ApplicationUser
            {
                Id = adminPlayer.Id,
                UserName = adminPlayer.Nickname,
                Email = "admin@tournamentz.com"
            };

            ApplicationUser prviUser = new ApplicationUser
            {
                Id = player1.Id,
                UserName = player1.Nickname,
                Email = "prvi.user@tournamentz.com"
            };

            ApplicationUser drugiUser = new ApplicationUser
            {
                Id = player2.Id,
                UserName = player2.Nickname,
                Email = "drugi.user@tournamentz.com"
            };

            ApplicationUser treciUser = new ApplicationUser
            {
                Id = player3.Id,
                UserName = player3.Nickname,
                Email = "treci.user@tournamentz.com"
            };

            userManager.Create(adminUser, "adminadmin");
            userManager.Create(prviUser, "prvi.user");
            userManager.Create(drugiUser, "drugi.user");
            userManager.Create(treciUser, "treci.user");

            userManager.AddToRole(adminUser.Id, "Admin");
            userManager.AddToRole(adminUser.Id, "User");
            userManager.AddToRole(prviUser.Id, "User");
            userManager.AddToRole(drugiUser.Id, "User");
            userManager.AddToRole(treciUser.Id, "User");

            uow.Commit();
        }
    }
}