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
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            modelBuilder.Entity<ApplicationRole>().ToTable("ApplicationRoles");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("ApplicationUserRoles");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("ApplicationUserLogins");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("ApplicationUserClaims");

            modelBuilder.Entity<ApplicationUser>()
                .HasKey(au => au.Id);

            modelBuilder.Entity<Player>()
                .HasOptional(p => p.ApplicationUser)
                .WithRequired(au => au.Player);
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<TournamentType> TournamentTypes { get; set; }

        public DbSet<TournamentTeam> TournamentTeams { get; set; }

        public DbSet<TournamentRound> TournamentRounds { get; set; }

        public DbSet<TournamentRoundParticipant> TournamentRoundParticipants { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamPlayer> TeamPlayers { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<GameType> GameTypes { get; set; }
    }
}