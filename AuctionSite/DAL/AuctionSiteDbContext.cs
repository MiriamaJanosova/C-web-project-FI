using DAL.Entities;
using DAL.Initializers;
using System.Data.Common;
using System.Data.Entity;
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

        public static string ConnectionString { get; } =
            "Server=tcp:pv179db.database.windows.net,1433;Initial Catalog=AuctionSite;Persist Security Info=False;User Id=marekch;Password=pv179DB21071996;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=300;";
        public AuctionSiteDbContext() 
            : base(ConnectionString)
        {
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

            modelBuilder.Entity<User>()
                .HasMany(t => t.UserRaisesForAuction)
                .WithRequired(a => a.User)
                .WillCascadeOnDelete(false);
            
            modelBuilder.Entity<UserRole>()
                .HasKey(p => new { p.RoleId, p.UserId });
                
            modelBuilder.Entity<Login>().HasKey(p => p.UserId);

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Role>()
                .ToTable("Roles");

            modelBuilder.Entity<UserRole>()
                .ToTable("UserRoles");

            modelBuilder.Entity<Login>()
                .ToTable("Logins");
        }


    }
}
