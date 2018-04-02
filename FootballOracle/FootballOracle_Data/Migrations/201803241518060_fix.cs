namespace FootballOracle_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forecasts",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    MatchId = c.Guid(nullable: false),
                    AccountId = c.Guid(nullable: false),
                    Forcast = c.String(nullable: false),
                    Coefficient = c.Double(nullable: false),
                    homeGoals = c.Int(nullable: false),
                    AwayGoals = c.Int(nullable: false),
                    PointsPlayed = c.Int(nullable: false),
                    IsOpen = c.Boolean(nullable: false),
                    IsSuccess = c.Boolean(nullable: false),
                    IsPlayed = c.Boolean(nullable: false),
                    UserForecast_Id = c.Guid(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserForecasts", t => t.UserForecast_Id)
                .Index(t => t.UserForecast_Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Forecasts");
            DropIndex("dbo.Forecasts", new[] { "UserForecast_Id" });
            DropForeignKey("dbo.Forecasts", "UserForecast_Id", "dbo.UserForecasts");
        }
    }
}
