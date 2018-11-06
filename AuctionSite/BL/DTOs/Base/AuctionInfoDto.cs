using System;
using BL.DTOs.Common;

namespace BL.DTOs.Base
{
    public class AuctionInfoDto : DtoBase
    {
        public string Name { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public double ActualPrice { get; set; }
    }
}