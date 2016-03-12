namespace Tournamentz.DAL
{
    using Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;

    public sealed class TournamentzModelContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public TournamentzModelContext()
            : this("name=TournamentzModelContext")
        {
        }

        public TournamentzModelContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new UserCreateDatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            modelBuilder.Entity<ApplicationRole>().ToTable("ApplicationRoles");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("ApplicationUserRoles");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("ApplicationUserLogins");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("ApplicationUserClaims");
        }

        public DbSet<TodoEntry> Todos { get; set; }
    }
}