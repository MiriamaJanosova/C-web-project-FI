using DAL.Entities;
using DAL.Initializers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class AuctionSiteDbContext : IdentityDbContext<User, Role, int, Login, UserRole, Claim>
    {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<Raise> Raises { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Image> Images { get; set; }

<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
        public static string ConnectionString { get; } =
            //"Server=tcp:pv179db.database.windows.net,1433;Initial Catalog=AuctionSite;Persist Security Info=False;User Id=marekch;Password=pv179DB21071996;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=300;";
            "Data source=(localdb)\\mssqllocaldb;Database=MySite;Trusted_Connection=True;MultipleActiveResultSets=true";
        public AuctionSiteDbContext() 
            : base(ConnectionString)
        {
            //Database.SetInitializer(new AuctionSiteDbInitializer());
            Database.SetInitializer(new ProductionInitializer());
            Database.CommandTimeout = 300;
        }
        
        public AuctionSiteDbContext(DbConnection connection) :
            base(connection, true)
        {
            Database.CreateIfNotExists();
            
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Auction>()
            //    .HasMany(t => t.AuctionedItems)
            //    .WithOptional(a => a.Auction)
            //    .WillCascadeOnDelete(false);
            modelBuilder.Entity<User>()
                .HasMany(t => t.UserRaisesForAuction)
                .WithRequired(a => a.UserWhoRaised)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<UserRole>()
                .HasKey(p => new { p.RoleId, p.UserId });
                
            modelBuilder.Entity<Login>().HasKey(p => p.UserId);

            
            //this is instead of DbSet<User>
            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Role>()
                .ToTable("Roles");

            modelBuilder.Entity<UserRole>()
                .ToTable("UserRoles");

            modelBuilder.Entity<Login>()
                .ToTable("Logins");
            //modelBuilder.Entity<User>()
            //    .HasRequired(a => a.Email);
        }


    }
}
