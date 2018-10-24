using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User : IEntity
    {
        public int ID { get; set; }

        [NotMapped] 
        public string TableName { get; } = "users";

        [Required, MaxLength(50)]
        public string Name { get; set; }
        
        [Required, MaxLength(50)]
        public string Surname { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        public List<Raise> UserRaisesForAuction { get; set; }
        
        public List<UserRole> UserRoles { get; set; }
        
        public List<Item> Inventory { get; set; }
        
        public List<Review> Reviews { get; set; }
        
        public List<Auction> AuctionsCreated { get; set; }
    }
}
