using DAL.Entities;
using DAL.Initializers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AuctionSiteDbContext : DbContext
    {
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<Raise> Raises { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public static string ConnectionString { get; } =
            "Server=tcp:pv179db.database.windows.net,1433;Initial Catalog=AuctionSite;Persist Security Info=False;User ID=marekch;Password=pv179DB21071996;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=300;";

        public AuctionSiteDbContext() 
            : base(ConnectionString)
        {
            Database.SetInitializer(new AuctionSiteDbInitializer());
            Database.CommandTimeout = 300;
        }
        
        public AuctionSiteDbContext(DbConnection connection) :
            base(connection, true)
        {
            Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Auction>()
                .HasMany(t => t.AuctionedItems)
                .WithRequired(a => a.InAuction)
                .WillCascadeOnDelete(false); 
            modelBuilder.Entity<User>()
                .HasMany(t => t.UserRaisesForAuction)
                .WithRequired(a => a.UserWhoRaised)
                .WillCascadeOnDelete(false);
        }

    }
}
