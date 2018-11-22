using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BL.Identity
{
    public class IdentityUserManager : UserManager<User, int>
    {
        public IdentityUserManager(IUserStore<User, int> store) : base(store)
        {
        }
    }
}
