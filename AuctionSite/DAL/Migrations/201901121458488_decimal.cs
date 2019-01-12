namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _decimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Auctions", "StartPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Auctions", "ActualPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Raises", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Raises", "Amount", c => c.Double(nullable: false));
            AlterColumn("dbo.Auctions", "ActualPrice", c => c.Double(nullable: false));
            AlterColumn("dbo.Auctions", "StartPrice", c => c.Double(nullable: false));
        }
    }
}
