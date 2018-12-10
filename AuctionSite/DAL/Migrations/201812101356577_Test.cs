namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auctions", "Image");
        }
    }
}
