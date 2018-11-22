using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNet.Identity;

namespace BL.Identity
{
    public class IdentityRoleManager : RoleManager<Role, int>
    {
        public IdentityRoleManager(IRoleStore<Role, int> store) : base(store)
        {
        }
    }
}
