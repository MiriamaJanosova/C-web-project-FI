using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

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
        
        public virtual List<Raise> UserRaisesForAuction { get; set; }
        
        public virtual List<UserRole> UserRoles { get; set; }
        
        public virtual List<Item> Inventory { get; set; }
        
        public virtual List<Review> Reviews { get; set; }
        
        public virtual List<Auction> AuctionsCreated { get; set; }
    }
}
