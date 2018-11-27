using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class User : IdentityUser<int,Login,UserRole,Claim>, IEntity
    {
        [NotMapped] 
        public string TableName { get; } = "users";

        public virtual List<Raise> UserRaisesForAuction { get; set; }
        
        public virtual List<UserRole> UserRoles { get; set; }
        
        public virtual List<Item> Inventory { get; set; }
        
        public virtual List<Review> Reviews { get; set; }
        
        public virtual List<Auction> AuctionsCreated { get; set; }
    }
}
