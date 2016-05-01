namespace Tournamentz.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTournamentTeamRelation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TournamentsTeams",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TournamentId = c.Guid(nullable: false),
                        TeamId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Tournaments", t => t.TournamentId, cascadeDelete: true)
                .Index(t => t.TournamentId)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TournamentsTeams", "TournamentId", "dbo.Tournaments");
            DropForeignKey("dbo.TournamentsTeams", "TeamId", "dbo.Teams");
            DropIndex("dbo.TournamentsTeams", new[] { "TeamId" });
            DropIndex("dbo.TournamentsTeams", new[] { "TournamentId" });
            DropTable("dbo.TournamentsTeams");
        }
    }
}
