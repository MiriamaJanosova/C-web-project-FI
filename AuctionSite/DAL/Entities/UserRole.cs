using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserRole : IEntity
    {
        public int ID { get; set; }

        [NotMapped] 
        public string TableName { get; } = "user_roles";
        
        public Role Role { get; set; }
        
        [Required]
        public int RoleID { get; set; }
        
        public User User { get; set; }
        
        [Required]
        public int UserID { get; set; }

    }
}
