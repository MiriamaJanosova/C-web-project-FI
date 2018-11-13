using System;
using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Infrastructure;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.UnitOfWork;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace DAL.Config
{
    public class EFInstaller : IWindsorInstaller
    {
        internal const string ConnectionString =
            "Server=tcp:pv179db.database.windows.net,1433;Initial Catalog=AuctionSite;Persist Security Info=False;User ID=marekch;Password=pv179DB21071996;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=300;";
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(() => new AuctionSiteDbContext())
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
    }
}