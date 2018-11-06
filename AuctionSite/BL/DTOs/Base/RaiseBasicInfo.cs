using System;
using BL.DTOs.Common;

namespace BL.DTOs.Base
{
    public class RaiseBasicInfo : DtoBase
    {
        public double Amount { get; set; }
        
        public string AuctionName { get; set; }
        
        public DateTime DateTime { get; set; }
    }
}