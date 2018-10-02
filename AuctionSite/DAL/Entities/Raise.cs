using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Raise
    {
        public int ID { get; set; }
        public double Amount { get; set; }
        public Auction RaiseForAuction { get; set; }
    }
}
