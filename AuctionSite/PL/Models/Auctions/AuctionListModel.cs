using System.Collections.Generic;
using BL.DTOs.Base;
using X.PagedList;


namespace PL.Models.Auctions
{
    public class AuctionListModel
    {
        public StaticPagedList<AuctionDto> Auctions { get; set; }

        public AuctionListModel(IEnumerable<AuctionDto> auctions, int page, int size, int total)
        {
            Auctions = new StaticPagedList<AuctionDto>(auctions, page, size, total);
        }

        public AuctionListModel()
        {
            
        }
    }
}