using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BL.Identity
{
    public class IdentityRoleStore : RoleStore<Role, int, UserRole>
    {
        public IdentityRoleStore() : base(new AuctionSiteDbContext()) // change
        {
        }
    }
}
