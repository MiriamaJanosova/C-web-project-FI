using System.Data.Entity;
using Castle.Windsor;
using DAL;
using EFTests.Config;
using NUnit.Framework;

namespace EFTests
{
    [SetUpFixture]
    public class Initializer
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();

        /// <summary>
        /// Initializes all Business Layer tests
        /// </summary>
        [OneTimeSetUp]
        public void InitializeBusinessLayerTests()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            Database.SetInitializer(new DropCreateDatabaseAlways<AuctionSiteDbContext>());          
            Container.Install(new EFTestsInstaller());
        }
    }
}
