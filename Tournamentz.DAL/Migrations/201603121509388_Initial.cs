namespace Tournamentz.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationRoles",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.ApplicationUserRoles",
                c => new
                {
                    UserId = c.Guid(nullable: false),
                    RoleId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.ApplicationRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Ime = c.String(nullable: false, maxLength: 50),
                    Prezime = c.String(nullable: false, maxLength: 50),
                    Oib = c.String(maxLength: 11),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.ApplicationUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.Guid(nullable: false),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.ApplicationUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserRoles", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserLogins", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserClaims", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.ApplicationUserRoles", "RoleId", "dbo.ApplicationRoles");
            DropIndex("dbo.ApplicationUserLogins", new[] { "UserId" });
            DropIndex("dbo.ApplicationUserClaims", new[] { "UserId" });
            DropIndex("dbo.ApplicationUsers", "UserNameIndex");
            DropIndex("dbo.ApplicationUserRoles", new[] { "RoleId" });
            DropIndex("dbo.ApplicationUserRoles", new[] { "UserId" });
            DropIndex("dbo.ApplicationRoles", "RoleNameIndex");
            DropTable("dbo.ApplicationUserLogins");
            DropTable("dbo.ApplicationUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.ApplicationUserRoles");
            DropTable("dbo.ApplicationRoles");
        }
    }
}