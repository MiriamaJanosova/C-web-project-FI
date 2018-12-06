using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.Base;

namespace PL.Models.Auctions
{
    public class MyAuctionsModel
    {
        public IEnumerable<AuctionDto> Ongoing { get; set; }

        public IEnumerable<AuctionDto> Ended { get; set; }

        public MyAuctionsModel(IEnumerable<AuctionDto> auctions)
        {
            bool ongoing(AuctionDto dto) => dto.StartDate < DateTime.Now && DateTime.Now < dto.EndDate;

            var grouping = auctions.GroupBy(ongoing)
                .ToDictionary(g => g.Key, g => g.AsEnumerable());

            Ongoing = grouping.ContainsKey(true) ? grouping[true] : new AuctionDto[] { };
            Ended = grouping.ContainsKey(false) ? grouping[false] : new AuctionDto[] { };
        }
    }
}