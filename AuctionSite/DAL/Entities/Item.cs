using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Item
    {
        public int ID { get; set; }
        [MaxLength(50),Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ItemCategory> HasCategories { get; set; }
        public User Owner { get; set; }
        [Required]
        public int OwnerID { get; set; }
        public Auction InAuction { get; set; }
        public int AuctionID { get; set; }
    }
}
