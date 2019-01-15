using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BL.Config;
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
        [CannotBeEmpty]
        public IList<int> SelectedItems { get; set; } = new List<int>();

        public IList<ItemDto> AvailableItems { get; set; } = new List<ItemDto>();
    }
}