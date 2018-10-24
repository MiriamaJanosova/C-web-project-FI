using System;

namespace BL.DTOs
{
    public class AuctionBasicInfo
    {
        public string Name { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public double ActualPrice { get; set; }
    }
}