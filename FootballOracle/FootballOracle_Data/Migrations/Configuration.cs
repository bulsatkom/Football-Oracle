namespace FootballOracle_Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FootballOracle_Data.FootballOracleDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(FootballOracle_Data.FootballOracleDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            if(context.Championship.Count() == 0)
            {
                var championship = new Championship()
                {
                    Id = Guid.NewGuid(),
                    Name = "Angliq"
                };

                context.Championship.Add(championship);
                context.SaveChanges();
            }
        }
    }
}
