namespace Tournamentz.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<TournamentzModelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}