using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public static class EntityFrameworkUnitOfWorkProviderFactory
    {
        public static IUnitOfWorkProvider Create()
        {
            return new EntityFrameworkUnitOfWorkProvider(() => new AuctionSiteDbContext());
        }
    }
}
