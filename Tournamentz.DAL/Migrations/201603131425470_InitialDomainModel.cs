namespace Tournamentz.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialDomainModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Title = c.String(nullable: false, maxLength: 150),
                    GameTypeId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameTypes", t => t.GameTypeId, cascadeDelete: true)
                .Index(t => t.GameTypeId);

            CreateTable(
                "dbo.GameTypes",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Title = c.String(nullable: false, maxLength: 150),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Players",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Nickname = c.String(nullable: false, maxLength: 50),
                    Name = c.String(maxLength: 50),
                    Surname = c.String(maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TeamsPlayers",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    TeamId = c.Guid(nullable: false),
                    PlayerId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId)
                .Index(t => t.PlayerId);

            CreateTable(
                "dbo.Teams",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Title = c.String(nullable: false, maxLength: 150),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.TournamentRoundsParticipants",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    TournamentRoundId = c.Guid(nullable: false),
                    TeamId = c.Guid(nullable: false),
                    Score = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.TournamentRounds", t => t.TournamentRoundId, cascadeDelete: true)
                .Index(t => t.TournamentRoundId)
                .Index(t => t.TeamId);

            CreateTable(
                "dbo.TournamentRounds",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    TournamentId = c.Guid(nullable: false),
                    GameId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId)
                .Index(t => t.GameId);

            CreateTable(
                "dbo.Tournaments",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Title = c.String(nullable: false, maxLength: 150),
                    OrganizerId = c.Guid(nullable: false),
                    TournamentTypeId = c.Guid(nullable: false),
                    StartDateTime = c.DateTime(nullable: false),
                    EndDateTime = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.OrganizerId, cascadeDelete: true)
                .ForeignKey("dbo.TournamentTypes", t => t.TournamentTypeId, cascadeDelete: true)
                .Index(t => t.OrganizerId)
                .Index(t => t.TournamentTypeId);

            CreateTable(
                "dbo.TournamentTypes",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(nullable: false, maxLength: 150),
                })
                .PrimaryKey(t => t.Id);

            CreateIndex("dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.ApplicationUsers", "Id", "dbo.Players", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.TournamentRoundsParticipants", "TournamentRoundId", "dbo.TournamentRounds");
            DropForeignKey("dbo.Tournaments", "TournamentTypeId", "dbo.TournamentTypes");
            DropForeignKey("dbo.TournamentRounds", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.Tournaments", "OrganizerId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.TournamentRounds", "GameId", "dbo.Games");
            DropForeignKey("dbo.TournamentRoundsParticipants", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamsPlayers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamsPlayers", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.ApplicationUsers", "Id", "dbo.Players");
            DropForeignKey("dbo.Games", "GameTypeId", "dbo.GameTypes");
            DropIndex("dbo.Tournaments", new[] { "TournamentTypeId" });
            DropIndex("dbo.Tournaments", new[] { "OrganizerId" });
            DropIndex("dbo.TournamentRounds", new[] { "GameId" });
            DropIndex("dbo.TournamentRounds", new[] { "TournamentId" });
            DropIndex("dbo.TournamentRoundsParticipants", new[] { "TeamId" });
            DropIndex("dbo.TournamentRoundsParticipants", new[] { "TournamentRoundId" });
            DropIndex("dbo.TeamsPlayers", new[] { "PlayerId" });
            DropIndex("dbo.TeamsPlayers", new[] { "TeamId" });
            DropIndex("dbo.ApplicationUsers", new[] { "Id" });
            DropIndex("dbo.Games", new[] { "GameTypeId" });
            DropTable("dbo.TournamentTypes");
            DropTable("dbo.Tournaments");
            DropTable("dbo.TournamentRounds");
            DropTable("dbo.TournamentRoundsParticipants");
            DropTable("dbo.Teams");
            DropTable("dbo.TeamsPlayers");
            DropTable("dbo.Players");
            DropTable("dbo.GameTypes");
            DropTable("dbo.Games");
        }
    }
}