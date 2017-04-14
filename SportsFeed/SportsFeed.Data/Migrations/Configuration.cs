using System.Data.Entity.Migrations;

namespace SportsFeed.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<SportsFeedDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SportsFeedDbContext context)
        {
        }
    }
}
