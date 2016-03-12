namespace Tournamentz.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedTestTodoEntries : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.CreatedById, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.LastUpdatedById)
                .Index(t => t.CreatedById)
                .Index(t => t.LastUpdatedById);
        }

        public override void Down()
        {
            DropForeignKey("dbo.TodoEntries", "LastUpdatedById", "dbo.ApplicationUsers");
            DropForeignKey("dbo.TodoEntries", "CreatedById", "dbo.ApplicationUsers");
            DropIndex("dbo.TodoEntries", new[] { "LastUpdatedById" });
            DropIndex("dbo.TodoEntries", new[] { "CreatedById" });
            DropTable("dbo.TodoEntries");
        }
    }
}