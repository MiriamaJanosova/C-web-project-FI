namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201812091439_Updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "UserWhoWroteID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "UserWhoWroteID");
        }
    }
}
