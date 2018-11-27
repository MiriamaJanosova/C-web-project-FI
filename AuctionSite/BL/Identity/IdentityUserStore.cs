using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Infrastructure.UnitOfWork;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BL.Identity
{
    public class IdentityUserStore : UserStore<User, Role, int, Login, UserRole, Claim>
    {
        public IdentityUserStore() : base(new AuctionSiteDbContext()) // change
        {
        }

        /**public IdentityUserStore(IUnitOfWorkProvider unitOfWorkProvider)
            : base((unitOfWorkProvider.GetUnitOfWorkInstance() as UnitOfWorkBase)?.Context)
        {
        }*/
    }
}
