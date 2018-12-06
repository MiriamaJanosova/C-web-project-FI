using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Common;

namespace BL.DTOs.Auction
{
    public class MyAuctions : DtoBase
    {
        public IEnumerable<AuctionDto> Ongoing { get; set; }

        public IEnumerable<AuctionDto> Ended { get; set; }
    }
}
