namespace FootballOracle_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Image = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        ChampionshipId = c.Guid(nullable: false),
                        Viewing = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                        Description = c.String(nullable: false, maxLength: 500),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TeamId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Championships",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChampionshipId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Matches = c.Int(nullable: false),
                        Wins = c.Int(nullable: false),
                        Draws = c.Int(nullable: false),
                        Losses = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        Picture = c.String(nullable: false),
                        GoalScored = c.Int(nullable: false),
                        GoalConcedered = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Championships", t => t.ChampionshipId, cascadeDelete: true)
                .Index(t => t.ChampionshipId);
            
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
            
            CreateTable(
                "dbo.ForumAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ForumPoints = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForumPosts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ThemeId = c.Guid(nullable: false),
                        ForumAccountId = c.Guid(nullable: false),
                        Description = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumAccounts", t => t.ForumAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Themes", t => t.ThemeId, cascadeDelete: true)
                .Index(t => t.ThemeId)
                .Index(t => t.ForumAccountId);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ForumPostId = c.Guid(nullable: false),
                        ForumAccountId = c.Guid(nullable: false),
                        IsLike = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumPosts", t => t.ForumPostId, cascadeDelete: true)
                .Index(t => t.ForumPostId);
            
            CreateTable(
                "dbo.ForumArticles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ThreadId = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Threads", t => t.ThreadId, cascadeDelete: true)
                .Index(t => t.ThreadId);
            
            CreateTable(
                "dbo.Themes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ForumAccountId = c.Guid(nullable: false),
                        TitleForumPost = c.Guid(nullable: false),
                        ForumArticle_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumArticles", t => t.ForumArticle_Id)
                .Index(t => t.ForumArticle_Id);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        HomeTeam = c.Guid(nullable: false),
                        AwayTeam = c.Guid(nullable: false),
                        ChampionshipId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsOpen = c.Boolean(nullable: false),
                        HomeGoals = c.Int(nullable: false),
                        AwayGoals = c.Int(nullable: false),
                        HomeCoefficient = c.Double(nullable: false),
                        DrawCoefficient = c.Double(nullable: false),
                        AwayCoefficient = c.Double(nullable: false),
                        PlayedFrom = c.Int(nullable: false),
                        PlayedFor1 = c.Int(),
                        PlayedForX = c.Int(),
                        PlayedFor2 = c.Int(),
                        SuccessMatch = c.Int(nullable: false),
                        SuccessResult = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserForecasts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagArticles",
                c => new
                    {
                        Tag_Id = c.Guid(nullable: false),
                        Article_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Article_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Article_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Forecasts", "UserForecast_Id", "dbo.UserForecasts");
            DropForeignKey("dbo.ForumArticles", "ThreadId", "dbo.Threads");
            DropForeignKey("dbo.Themes", "ForumArticle_Id", "dbo.ForumArticles");
            DropForeignKey("dbo.ForumPosts", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.ForumPosts", "ForumAccountId", "dbo.ForumAccounts");
            DropForeignKey("dbo.Likes", "ForumPostId", "dbo.ForumPosts");
            DropForeignKey("dbo.Teams", "ChampionshipId", "dbo.Championships");
            DropForeignKey("dbo.TagArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.TagArticles", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropIndex("dbo.TagArticles", new[] { "Article_Id" });
            DropIndex("dbo.TagArticles", new[] { "Tag_Id" });
            DropIndex("dbo.Themes", new[] { "ForumArticle_Id" });
            DropIndex("dbo.ForumArticles", new[] { "ThreadId" });
            DropIndex("dbo.Likes", new[] { "ForumPostId" });
            DropIndex("dbo.ForumPosts", new[] { "ForumAccountId" });
            DropIndex("dbo.ForumPosts", new[] { "ThemeId" });
            DropIndex("dbo.Forecasts", new[] { "UserForecast_Id" });
            DropIndex("dbo.Teams", new[] { "ChampionshipId" });
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            DropTable("dbo.TagArticles");
            DropTable("dbo.UserForecasts");
            DropTable("dbo.Threads");
            DropTable("dbo.Matches");
            DropTable("dbo.Themes");
            DropTable("dbo.ForumArticles");
            DropTable("dbo.Likes");
            DropTable("dbo.ForumPosts");
            DropTable("dbo.ForumAccounts");
            DropTable("dbo.Forecasts");
            DropTable("dbo.Teams");
            DropTable("dbo.Championships");
            DropTable("dbo.Tags");
            DropTable("dbo.Comments");
            DropTable("dbo.Articles");
        }
    }
}
