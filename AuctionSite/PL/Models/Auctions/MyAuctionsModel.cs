using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.Base;
using DAL.Entities;
using X.PagedList;

namespace PL.Models.Auctions
{
    public class MyAuctionsModel
    {
        public AuctionListModel Ongoing { get; set; } = new AuctionListModel();

        public AuctionListModel Ended { get; set; } = new AuctionListModel();

        public MyAuctionsModel(IEnumerable<AuctionDto> auctions)
        {
            bool ongoing(AuctionDto dto) => dto.StartDate < DateTime.Now && DateTime.Now < dto.EndDate;

            var grouping = auctions.GroupBy(ongoing)
                .ToDictionary(g => g.Key, g => g.AsEnumerable());
            
            
            if (grouping.ContainsKey(true))
            {
                Ongoing.Auctions = new StaticPagedList<AuctionDto>(grouping[true], 1, 15, grouping[true].Count());
            }

            if (grouping.ContainsKey(false))
            {
                Ended.Auctions = new StaticPagedList<AuctionDto>(grouping[false], 1, 15, grouping[false].Count());
            }
            
            
           
        }
    }
}