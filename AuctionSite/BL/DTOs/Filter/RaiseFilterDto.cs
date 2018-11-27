using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Common;

namespace BL.DTOs.Filter
{
    public class RaiseFilterDto : FilterDtoBase
    {
        public double Amount { get; set; }

        public int RaiseForAuctionID { get; set; }

        public int AuctionerID { get; set; }


    }
}
