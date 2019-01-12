using DAL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Initializers
{
    public class AuctionSiteDbInitializer : DropCreateDatabaseAlways<AuctionSiteDbContext>
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
                new Currency{Code = "CZK", ExchangeRate = 25},
                new Currency{Code = "USD", ExchangeRate = 5}
            };

            context.Currencies.AddRange(currencies);
            context.SaveChanges();

            var roles = new List<Role>
            {
                new Role { Name = "Admin" },
                new Role { Name = "RegisteredUser" },
                new Role { Name = "User" }
            };

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }

            context.SaveChanges();

            var users = new List<User>
            {
                new User { UserName = "Peter Novotný", Email = "prvni@email.com" },
                new User { UserName = "Jaroslav Dlouhý", Email = "druhy@email.com" },
                new User { UserName = "Ladislav Krátky", Email = "treti@email.com" },
                new User { UserName = "Karolína Milová", Email = "ctvrty@email.com" },
                
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

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
                new Category {CategoryType = "Art"},
                new Category {CategoryType = "Electronic appliances"},
                new Category {CategoryType = "Furniture"},
                new Category {CategoryType = "Random house stuff"},
                new Category {CategoryType = "Vintage"}
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var auctions = new List<Auction>
            {
                new Auction {StartDate = DateTime.Parse("2017-08-15"), EndDate = DateTime.Parse("2017-08-23"), ActualPrice = 400, UserId = 1, Name = "bla", Description = "blablass"},
                new Auction {StartDate = DateTime.Parse("2017-10-28"), EndDate = DateTime.Parse("2017-11-03"), ActualPrice = 185.6m, UserId = 1, Name = "bla2", Description = "blabla"}
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


            var raises = new List<Raise>
            {
                new Raise {Amount = 200, AuctionId = 1, UserId = 2, DateTime = DateTime.Parse("2017-08-16")},
                new Raise {Amount = 300, AuctionId = 1, UserId = 3, DateTime = DateTime.Parse("2017-08-17")},
                new Raise {Amount = 400, AuctionId = 1, UserId = 2, DateTime = DateTime.Parse("2017-08-22")},
                new Raise {Amount = 185, AuctionId = 1, UserId = 2, DateTime = DateTime.Parse("2017-10-30")},
                new Raise {Amount = 400, AuctionId = 1, UserId = 3, DateTime = DateTime.Parse("2017-11-03")}
            };

            context.Raises.AddRange(raises);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
