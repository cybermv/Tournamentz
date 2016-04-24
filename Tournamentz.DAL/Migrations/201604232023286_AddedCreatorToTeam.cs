namespace Tournamentz.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCreatorToTeam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "CreatorId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Teams", "CreatorId");
            AddForeignKey("dbo.Teams", "CreatorId", "dbo.ApplicationUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "CreatorId", "dbo.ApplicationUsers");
            DropIndex("dbo.Teams", new[] { "CreatorId" });
            DropColumn("dbo.Teams", "CreatorId");
        }
    }
}
