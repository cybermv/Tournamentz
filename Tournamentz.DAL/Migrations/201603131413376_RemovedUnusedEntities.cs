namespace Tournamentz.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedUnusedEntities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TodoEntries", "CreatedById", "dbo.ApplicationUsers");
            DropForeignKey("dbo.TodoEntries", "LastUpdatedById", "dbo.ApplicationUsers");
            DropIndex("dbo.TodoEntries", new[] { "CreatedById" });
            DropIndex("dbo.TodoEntries", new[] { "LastUpdatedById" });
            DropColumn("dbo.ApplicationUsers", "Ime");
            DropColumn("dbo.ApplicationUsers", "Prezime");
            DropColumn("dbo.ApplicationUsers", "Oib");
            DropTable("dbo.TodoEntries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TodoEntries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Difficulty = c.Int(nullable: false),
                        CreatedById = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        LastUpdatedById = c.Guid(),
                        LastUpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ApplicationUsers", "Oib", c => c.String(maxLength: 11));
            AddColumn("dbo.ApplicationUsers", "Prezime", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.ApplicationUsers", "Ime", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.TodoEntries", "LastUpdatedById");
            CreateIndex("dbo.TodoEntries", "CreatedById");
            AddForeignKey("dbo.TodoEntries", "LastUpdatedById", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.TodoEntries", "CreatedById", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
        }
    }
}
