namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DontTouchThisMotherfuckerORIWillFindYouAndKillYou : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "ImageBytes", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "ImageBytes");
        }
    }
}
