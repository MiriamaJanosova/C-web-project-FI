using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DAL;
using DAL.Entities;
using Infrastructure;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.UnitOfWork;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;
using Component = Castle.MicroKernel.Registration.Component;

namespace EFTests.Config
{
    public class EFTestsInstaller : IWindsorInstaller
    {
        private const string TestDbConnection = "InMemoryDb";

        #region UsersConfig

        public static User User1 { get; } = new User
        {
            UserName = "Pepa Stok",
            Email = "pepastok@fernet.com"
        };

        public static User User2 { get; } = new User
        {
            UserName = "Bořetěch idk",
            Email = "boretechuv@email.cz"
        };

        #endregion

        #region ItemsConfig

        public static Item ItemUser1 = new Item()
        {
            Name = "Fernet Štok",
            Description = "pouze otevreno, ochutnano, zavreno"
        };

        public static Item ItemUser2 = new Item()
        {
            Name = "Becherovka",
            Description = "Trochu upito, ale jinak je v pohode"
        };

        #endregion
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(InitializeDatabase)
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EntityFrameworkUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EntityFrameworkRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EntityFrameworkQuery<>))
                    .LifestyleTransient()
            );
        }

        private static DbContext InitializeDatabase()
        {
            var dbCxt = new AuctionSiteDbContext(Effort.DbConnectionFactory.CreatePersistent(TestDbConnection));
            foreach (var dbCxtUser in dbCxt.Users)
            {
                dbCxt.Users.Remove(dbCxtUser);
            }

            
            dbCxt.Items.RemoveRange(dbCxt.Items);
            dbCxt.Categories.RemoveRange(dbCxt.Categories);
            dbCxt.ItemCategories.RemoveRange(dbCxt.ItemCategories);
            dbCxt.EmailTemplates.RemoveRange(dbCxt.EmailTemplates);
            
            dbCxt.SaveChanges();

            #region Categories
            var alcohol = new Category
            {
                CategoryType = "Alcohol",
                Description = "Vse, co obsahuje znamky alkoholu"
            };

            
            var randomHouseStuff = new Category
            {
                CategoryType = "RandomHouseStuff",
                Description = "veci z baraku"
            };

            dbCxt.Categories.AddOrUpdate(alcohol);
            dbCxt.Categories.AddOrUpdate(randomHouseStuff);
            
            #endregion
            
            #region Users

            dbCxt.Users.AddOrUpdate(User1);
            dbCxt.Users.AddOrUpdate(User2);
            
            #endregion

            dbCxt.SaveChanges();
            
            #region Items


            ItemUser1.Owner = User1;
            ItemUser1.OwnerID = User1.Id;
            ItemUser2.Owner = User2;
            ItemUser2.OwnerID = User2.Id;
            
            dbCxt.Items.AddOrUpdate(ItemUser1);
            dbCxt.Items.AddOrUpdate(ItemUser2);
            #endregion

            dbCxt.SaveChanges();

            #region Categories

            ItemUser1.HasCategories = new List<ItemCategory>
            {
                new ItemCategory
                {
                    Category = alcohol,
                    CategoryID = alcohol.Id,
                    Item = ItemUser1,
                    ItemID = ItemUser1.Id
                }
            };
            
            ItemUser2.HasCategories = new List<ItemCategory>
            {
                new ItemCategory
                {
                    Category = alcohol,
                    CategoryID = alcohol.Id,
                    Item = ItemUser2,
                    ItemID = ItemUser2.Id
                }, new ItemCategory
                {
                    Category = randomHouseStuff,
                    CategoryID = randomHouseStuff.Id,
                    Item = ItemUser2,
                    ItemID = ItemUser2.Id
                }
            };

            #endregion
            
            dbCxt.SaveChanges();
            return dbCxt;
        }
    }
}