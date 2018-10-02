using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace DAL.Entities

{
    public class Auction
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double ActualPrice { get; set; }
        public User Auctioner { get; set; }
        public List<User> AuctionParticipants { get; set; }
        public List<Item> AuctionedItems { get; set; }

    }
    
}
