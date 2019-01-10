using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.Base;
using X.PagedList;

namespace PL.Models.Auctions
{
    public class MyAuctionsModel
    {
        public AuctionListModel Ongoing { get; set; } = new AuctionListModel(null);

        public AuctionListModel Ended { get; set; } = new AuctionListModel(null);

        public MyAuctionsModel(IEnumerable<AuctionDto> auctions)
        {
            bool ongoing(AuctionDto dto) => dto.StartDate < DateTime.Now && DateTime.Now < dto.EndDate;

            var grouping = auctions.GroupBy(ongoing)
                .ToDictionary(g => g.Key, g => g.AsEnumerable());
            
            
            if (grouping.ContainsKey(true))
            {
                Ongoing.Auctions = grouping[true].ToPagedList();
            }

            if (grouping.ContainsKey(false))
            {
                Ended.Auctions = grouping[false].ToPagedList();
            }
            
            
           
        }
    }
}