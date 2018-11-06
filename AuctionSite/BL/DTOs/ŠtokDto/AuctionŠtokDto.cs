using System;
using System.Collections.Generic;
using BL.DTOs.Common;
using DAL.Entities;

namespace BL.DTOs.ŠtokDto
{
    public class AuctionŠtokDto : DtoBase
    {
        public string Name { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public string Description { get; set; }
        
        public double ActualPrice { get; set; }
        
        public List<Item> AuctionedItems { get; set; }

        public int UserId { get; set; }
    }
}