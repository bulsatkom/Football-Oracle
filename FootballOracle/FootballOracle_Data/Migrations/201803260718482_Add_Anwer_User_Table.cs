namespace FootballOracle_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Anwer_User_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerUsers",
                c => new
                    {
                        AnswerId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AnswerUsers");
        }
    }
}
