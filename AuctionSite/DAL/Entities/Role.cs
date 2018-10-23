using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Role : IEntity
    {
        public int ID { get; set; }

        [NotMapped] 
        public string TableName { get; } = "roles";

        [Required]
        public UserRoleType RoleType { get; set; }
        
        public List<UserRole> UsersOfRole { get; set; }
    }
}
