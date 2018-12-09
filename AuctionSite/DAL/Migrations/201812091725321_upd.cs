namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "Evaluation", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Evaluation", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
