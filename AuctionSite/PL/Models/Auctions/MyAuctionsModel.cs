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
        
        public int PageOn { get; set; }
        
        public int PageEn { get; set; }

        public MyAuctionsModel(IEnumerable<AuctionDto> auctions, int pageEn, int pageOn)
        {
            bool ongoing(AuctionDto dto) => dto.StartDate < DateTime.Now && DateTime.Now < dto.EndDate;

            PageOn = pageOn;

            PageEn = pageEn;
            
            var grouping = auctions.GroupBy(ongoing)
                .ToDictionary(g => g.Key, g => g.AsEnumerable());
            
            
            if (grouping.ContainsKey(true))
            {
                var totalOn = grouping[true].Count();
                Ongoing.Auctions = new StaticPagedList<AuctionDto>(grouping[true].Skip((pageOn - 1) * 15).Take(15), pageOn, 15, totalOn);
            }

            if (grouping.ContainsKey(false))
            {
                var totalEn = grouping[false].Count();
                Ended.Auctions = new StaticPagedList<AuctionDto>(grouping[false].Skip((pageEn - 1) * 15).Take(15), pageEn, 15, totalEn);
            }
            
            
           
        }
    }
}