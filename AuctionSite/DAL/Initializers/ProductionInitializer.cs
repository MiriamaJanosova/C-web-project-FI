using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PasswordHasher = DAL.Helper.PasswordHasher;

namespace DAL.Initializers
{
    public class ProductionInitializer : CreateDatabaseIfNotExists<AuctionSiteDbContext>
    {
        
        protected override void Seed(AuctionSiteDbContext context)
        {
            #region RoleManager
            var store = new RoleStore<Role, int, UserRole>(context);
            var manager = new RoleManager<Role, int>(store);

            var roles = new List<Role>
            {
                new Role()
                {
                    Name = "Admin"
                },
                new Role()
                {
                    Name = "User"
                }
            };

            foreach (var role in roles)
            {
                manager.Create(role);
            }

            context.SaveChanges();
            #endregion

            #region UserManager
            
            var userStore = new UserStore<User, Role, int, Login, UserRole, Claim>(context);
            var userManager = new UserManager<User, int>(userStore);
            var admin = new User
            {
                UserName = "admin"
            };

            userManager.Create(admin, "123456789");
            userManager.AddToRole(admin.Id, "Admin");
            #endregion

            #region Categories

            var categories = new List<Category>
            {
                new Category
                {
                    CategoryType = "Random House Stuff",
                    Description = "Random stuffs from  your house"
                },
                new Category
                {
                    CategoryType = "Alcohol",
                    Description = "Everything, which has alcohol"
                }
            };

            context.Categories.AddRange(categories);

            #endregion
            
            context.SaveChanges();
            
        }
    }
}