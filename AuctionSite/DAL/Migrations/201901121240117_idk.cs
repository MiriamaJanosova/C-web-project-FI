namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idk : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Currencies", "ExchangeRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Currencies", "ExchangeRate", c => c.Double(nullable: false));
        }
    }
}
