namespace FootballOracle_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_AnswerUser_Table : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AnswerUsers");
            AddColumn("dbo.AnswerUsers", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.AnswerUsers", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.AnswerUsers");
            DropColumn("dbo.AnswerUsers", "Id");
            AddPrimaryKey("dbo.AnswerUsers", "AnswerId");
        }
    }
}
