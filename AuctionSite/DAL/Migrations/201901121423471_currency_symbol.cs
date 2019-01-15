namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currency_symbol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Currencies", "Symbol", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Currencies", "Code", c => c.String(nullable: false, maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Currencies", "Code", c => c.String(maxLength: 5));
            DropColumn("dbo.Currencies", "Symbol");
        }
    }
}
