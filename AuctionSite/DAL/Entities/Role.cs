using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Role
    {
        public int ID { get; set; }
        [Required]
        public UserRoleType RoleType { get; set; }
        public List<UserRole> UsersOfRole { get; set; }
    }
}
