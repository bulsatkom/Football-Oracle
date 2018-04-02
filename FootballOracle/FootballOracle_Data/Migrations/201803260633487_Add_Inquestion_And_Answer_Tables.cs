namespace FootballOracle_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Inquestion_And_Answer_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InquestionId = c.Guid(nullable: false),
                        Text = c.String(nullable: false),
                        playedFrom = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Inquestions", t => t.InquestionId, cascadeDelete: true)
                .Index(t => t.InquestionId);
            
            CreateTable(
                "dbo.Inquestions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Question = c.String(nullable: false),
                        PlayersCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "InquestionId", "dbo.Inquestions");
            DropIndex("dbo.Answers", new[] { "InquestionId" });
            DropTable("dbo.Inquestions");
            DropTable("dbo.Answers");
        }
    }
}
