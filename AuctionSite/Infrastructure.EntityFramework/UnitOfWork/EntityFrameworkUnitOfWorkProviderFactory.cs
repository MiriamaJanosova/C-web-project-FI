using DAL;
using Infrastructure.UnitOfWork;

namespace Infrastructure.EntityFramework.UnitOfWork
{
    public static class EntityFrameworkUnitOfWorkProviderFactory
    {
        public static IUnitOfWorkProvider Create()
        {
            return new EntityFrameworkUnitOfWorkProvider(() => new AuctionSiteDbContext());
        }
    }
}
