namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auctions", "ImageBytes", c => c.Binary());
            DropColumn("dbo.Auctions", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auctions", "Image", c => c.Binary());
            DropColumn("dbo.Auctions", "ImageBytes");
        }
    }
}
