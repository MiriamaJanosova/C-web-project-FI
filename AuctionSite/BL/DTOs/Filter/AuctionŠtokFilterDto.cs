using System;
using BL.DTOs.Common;

namespace BL.DTOs.Filter
{
    public class AuctionŠtokFilterDto : FilterŠtokDtoBase
    {
        public string AuctionName { get; set; }
        //TODO
        //public string[] ItemNames { get; set; }

        public double MinimalPrice { get; set; } = 0;  

        public double MaximalPrice { get; set; } = double.MaxValue;
        
        
    }
}