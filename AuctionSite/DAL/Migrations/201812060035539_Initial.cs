namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auctions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ActualPrice = c.Double(nullable: false),
                        AuctionerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuctionerID, cascadeDelete: true)
                .Index(t => t.AuctionerID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        OwnerID = c.Int(nullable: false),
                        AuctionID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auctions", t => t.AuctionID)
                .ForeignKey("dbo.Users", t => t.OwnerID, cascadeDelete: true)
                .Index(t => t.OwnerID)
                .Index(t => t.AuctionID);
            
            CreateTable(
                "dbo.ItemCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .Index(t => t.ItemID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryType = c.Int(nullable: false),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Evaluation = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        ReviewedUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.ReviewedUserID, cascadeDelete: true)
                .Index(t => t.ReviewedUserID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        User_Id = c.Int(),
                        User_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.Raises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        RaiseForAuctionID = c.Int(nullable: false),
                        UserWhoRaisedID = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auctions", t => t.RaiseForAuctionID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserWhoRaisedID)
                .Index(t => t.RaiseForAuctionID)
                .Index(t => t.UserWhoRaisedID);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 5),
                        ExchangeRate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Auctions", "AuctionerID", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.Raises", "UserWhoRaisedID", "dbo.Users");
            DropForeignKey("dbo.Raises", "RaiseForAuctionID", "dbo.Auctions");
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Reviews", "ReviewedUserID", "dbo.Users");
            DropForeignKey("dbo.Logins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Items", "OwnerID", "dbo.Users");
            DropForeignKey("dbo.Claims", "UserId", "dbo.Users");
            DropForeignKey("dbo.ItemCategories", "ItemID", "dbo.Items");
            DropForeignKey("dbo.ItemCategories", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Items", "AuctionID", "dbo.Auctions");
            DropIndex("dbo.Raises", new[] { "UserWhoRaisedID" });
            DropIndex("dbo.Raises", new[] { "RaiseForAuctionID" });
            DropIndex("dbo.UserRoles", new[] { "User_Id1" });
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Reviews", new[] { "ReviewedUserID" });
            DropIndex("dbo.Logins", new[] { "UserId" });
            DropIndex("dbo.Claims", new[] { "UserId" });
            DropIndex("dbo.ItemCategories", new[] { "CategoryID" });
            DropIndex("dbo.ItemCategories", new[] { "ItemID" });
            DropIndex("dbo.Items", new[] { "AuctionID" });
            DropIndex("dbo.Items", new[] { "OwnerID" });
            DropIndex("dbo.Auctions", new[] { "AuctionerID" });
            DropTable("dbo.Roles");
            DropTable("dbo.EmailTemplates");
            DropTable("dbo.Currencies");
            DropTable("dbo.Raises");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.Logins");
            DropTable("dbo.Claims");
            DropTable("dbo.Users");
            DropTable("dbo.Categories");
            DropTable("dbo.ItemCategories");
            DropTable("dbo.Items");
            DropTable("dbo.Auctions");
        }
    }
}
