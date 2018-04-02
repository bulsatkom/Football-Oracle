namespace FootballOracle_Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit_Inquestion_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inquestions", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inquestions", "IsActive");
        }
    }
}
