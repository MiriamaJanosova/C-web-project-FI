using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Raise : IEntity
    {
        public int ID { get; set; }
        [Required]
        public double Amount { get; set; }
        public Auction RaiseForAuction { get; set; }
        [Required]
        public int RaiseForAuctionID { get; set; }
        public User UserWhoRaised { get; set; }
        [Required]
        public int UserWhoRaisedID { get; set; }
        public DateTime DateTime { get; set; }
    }
}
