using System.Collections.Generic;
using BL.DTOs.Base;
using X.PagedList;


namespace PL.Models.Auctions
{
    public class AuctionListModel
    {
        public IPagedList<AuctionDto> Auctions { get; set; }

        public AuctionListModel(IEnumerable<AuctionDto> auctions)
        {
            Auctions = auctions?.ToPagedList();
        }
    }
}