using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Initializers
{
    public class AuctionSiteDbInitializer : DropCreateDatabaseIfModelChanges<AuctionSiteDbContext>
    {
        protected override void Seed(AuctionSiteDbContext context)
        {
            context.EmailTemplates.Add(new EmailTemplate
            {
                Message = "Nejaka zprava"
            });

            var currencies = new List<Currency>
            {

                new Currency{Code = "EUR", ExchangeRate = 1},
                new Currency{Code = "CZK", ExchangeRate = 25.5},
                new Currency{Code = "USD", ExchangeRate = 1.15}
            };

            context.Currencies.AddRange(currencies);
            context.SaveChanges();

            var roles = new List<Role>
            {
                //new Role { RoleType = UserRoleType.Admin },
                //new Role { RoleType = UserRoleType.RegisteredUser },
                //new Role { RoleType = UserRoleType.User }
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();

            var users = new List<User>
            {
                new User { Name = "Peter", Surname = "Novotný", Email = "prvni@email.com" },
                new User { Name = "Jaroslav", Surname = "Dlouhý", Email = "druhy@email.com" },
                new User { Name = "Ladislav", Surname = "Krátky", Email = "treti@email.com" },
                new User { Name = "Karolína", Surname = "Milová", Email = "ctvrty@email.com" }
            }; 

            context.Users.AddRange(users);
            context.SaveChanges();

            var reviews = new List<Review>
            {
                new Review { Evaluation = 6, Description = "Celkom v OK, viem si ale predstaviť lepšiu komunikáciu.", ReviewedUserID = 1 },
                new Review { Evaluation = 0, Description = "Naneštastie mi poslal úplne inú vec, žiadal so vrátenie peňazí, nikdy som ich nedostal späť.", ReviewedUserID = 1 },
                new Review { Evaluation = 1, Description = "Spodne pradlo bylo zjavne pouzite a docela zapachalo", ReviewedUserID = 2 }
            };

            context.Reviews.AddRange(reviews);
            context.SaveChanges();

            var categories = new List<Category>
            {
                new Category {CategoryType = ItemCategoryType.Art},
                new Category {CategoryType = ItemCategoryType.ElectronicAppliances},
                new Category {CategoryType = ItemCategoryType.Furniture},
                new Category {CategoryType = ItemCategoryType.RandomHouseStuff},
                new Category {CategoryType = ItemCategoryType.Vintage}
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var auctions = new List<Auction>
            {
                new Auction {StartDate = DateTime.Parse("2017-08-15"), EndDate = DateTime.Parse("2017-08-23"), ActualPrice = 400, AuctionerID = 1, Name = "bla", Description = "blablass"},
                new Auction {StartDate = DateTime.Parse("2017-10-28"), EndDate = DateTime.Parse("2017-11-03"), ActualPrice = 185.6, AuctionerID = 1, Name = "bla2", Description = "blabla"}
            };

            context.Auctions.AddRange(auctions);
            context.SaveChanges();

            var items = new List<Item>
            {
                new Item {Name= "Obraz zátišia lesa", Description = "Krásny, nádhera, musíte to mať.", OwnerID = 1, AuctionID = 1},
                new Item {Name="Nový holiaci strojček", Description = "Braun, tá najlepšia kvalita.", OwnerID = 1, AuctionID = 1},
                new Item {Name="Stôl a stoličky", Description = "Kvalitný drevený nábytok, osvieži váš dom.", OwnerID = 1, AuctionID = 2}
            };

            context.Items.AddRange(items);
            context.SaveChanges();

            var itemCategories = new List<ItemCategory>
            {
                new ItemCategory {CategoryID = 1, ItemID = 1},
                new ItemCategory {CategoryID = 2, ItemID = 2},
                new ItemCategory {CategoryID = 3, ItemID = 3}
            };

            context.ItemCategories.AddRange(itemCategories);
            context.SaveChanges();

            //var userRoles = new List<UserRole>
            //{
            //    new UserRole {RoleID = 1, UserID = 1},
            //    new UserRole {RoleID = 2, UserID = 2},
            //    new UserRole {RoleID = 2, UserID = 3},
            //    new UserRole {RoleID = 2, UserID = 4}
            //};

            //context.UserRoles.AddRange(userRoles);
            //context.SaveChanges();

            var raises = new List<Raise>
            {
                new Raise {Amount = 200, RaiseForAuctionID = 1, UserWhoRaisedID = 2, DateTime = DateTime.Parse("2017-08-16")},
                new Raise {Amount = 300, RaiseForAuctionID = 1, UserWhoRaisedID = 3, DateTime = DateTime.Parse("2017-08-17")},
                new Raise {Amount = 400, RaiseForAuctionID = 1, UserWhoRaisedID = 2, DateTime = DateTime.Parse("2017-08-22")},
                new Raise {Amount = 185, RaiseForAuctionID = 1, UserWhoRaisedID = 2, DateTime = DateTime.Parse("2017-10-30")},
                new Raise {Amount = 400, RaiseForAuctionID = 1, UserWhoRaisedID = 3, DateTime = DateTime.Parse("2017-11-03")}
            };

            context.Raises.AddRange(raises);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
