namespace Tournamentz.DAL
{
    using Core;
    using Entity;
    using Identity;
    using Microsoft.AspNet.Identity;
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
            /*if (context.Users.Any()) { return; }

            BasicUnitOfWork uow = new BasicUnitOfWork(context);

            ApplicationRole adminRole = new ApplicationRole { Name = "Admin" };
            ApplicationRole userRole = new ApplicationRole { Name = "User" };

            IRepository<ApplicationRole> roleRepo = uow.Repository<ApplicationRole>();

            roleRepo.Insert(adminRole);
            roleRepo.Insert(userRole);

            ApplicationUserManager userManager = new ApplicationUserManager(uow);

            ApplicationUser adminUser = new ApplicationUser
            {
                UserName = "admin",
                Ime = "System",
                Prezime = "Admin",
                Oib = "12345654321",
                Email = "admin@tournamentz.com"
            };

            ApplicationUser prviUser = new ApplicationUser
            {
                UserName = "prvi.user",
                Ime = "Prvi",
                Prezime = "User",
                Oib = "00000111111",
                Email = "prvi.user@tournamentz.com"
            };

            ApplicationUser drugiUser = new ApplicationUser
            {
                UserName = "drugi.user",
                Ime = "Drugi",
                Prezime = "User",
                Oib = "00000222222",
                Email = "drugi.user@tournamentz.com"
            };

            ApplicationUser treciUser = new ApplicationUser
            {
                UserName = "treci.user",
                Ime = "Treci",
                Prezime = "User",
                Oib = "00000333333",
                Email = "treci.user@tournamentz.com"
            };

            userManager.Create(adminUser, "adminadmin");
            userManager.Create(prviUser, "prvi.user");
            userManager.Create(drugiUser, "drugi.user");
            userManager.Create(treciUser, "treci.user");

            userManager.AddToRole(adminUser.Id, "Admin");
            userManager.AddToRole(prviUser.Id, "User");
            userManager.AddToRole(drugiUser.Id, "User");
            userManager.AddToRole(treciUser.Id, "User");

            uow.Commit();*/
        }
    }
}