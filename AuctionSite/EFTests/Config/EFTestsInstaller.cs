using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace EFTests.Config
{
    public class EFTestsInstaller : IWindsorInstaller
    {
        private const string TestDbConnection = "InMemoryDb";
        
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
            dbCxt.Users.RemoveRange(dbCxt.Users);
            dbCxt.EmailTemplates.RemoveRange(dbCxt.EmailTemplates);
            dbCxt.SaveChanges();

            var randomHouseStuff = new Category
            {
                CategoryType = ItemCategoryType.RandomHouseStuff,
                Description = "veci z baraku"
            };
                
            var randomStuffs = new ItemCategory
            {
                Category = randomHouseStuff,
            };
            
            var user1 = new User
            {
                Name = "Pepa",
                Surname = "Štok",
                Email = "pepastok@fernet.com"
            };

            var user2 = new User
            {
                Name = "Bořetěch",
                Surname = "idk",
                Email = "boretechuv@email.cz"
            };

            dbCxt.Categories.Add(randomHouseStuff);
            dbCxt.ItemCategories.Add(randomStuffs);

            var itemUser1 = new Item()
            {
                Name = "Fernet Štok",
                Description = "pouze otevreno, ochutnano, zavreno",
                HasCategories = new List<ItemCategory>
                {
                    randomStuffs 
                    
                },
                Owner = user1,
                OwnerID = user1.ID
                    
            };

            return null;
        }
    }
}