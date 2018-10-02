using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Role
    {
        public int ID { get; set; }
        public UserRoleType RoleType { get; set; }
    }
}
