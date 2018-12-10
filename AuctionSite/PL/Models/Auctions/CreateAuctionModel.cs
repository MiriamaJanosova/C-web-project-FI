using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.Auction;
using BL.DTOs.Base;
using BL.DTOs.Item;

namespace PL.Models.Auctions
{
    public class CreateAuctionModel
    {
        public CreateAuction Dto { get; set; } = new CreateAuction()
        {
            EndDate = DateTime.Now.Add(TimeSpan.FromDays(1)),
            StartDate = DateTime.Now
        };

        public IList<int> SelectedItems { get; set; } = new List<int>();

        public IList<ItemDto> AvailableItems { get; set; } = new List<ItemDto>();
    }
}