using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class UserRole : IEntity
    {
        public int ID { get; set; }
        public Role Role { get; set; }
        [Required]
        public int RoleID { get; set; }
        public User User { get; set; }
        [Required]
        public int UserID { get; set; }

    }
}
