namespace FootballOracle_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_UserForecastTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserForecasts", "Points", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserForecasts", "Points");
        }
    }
}
