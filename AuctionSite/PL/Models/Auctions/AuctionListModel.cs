using System.Collections.Generic;
using System.Web.Mvc;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using X.PagedList;


namespace PL.Models.Auctions
{
    public class AuctionListModel
    {
        
        public string[] SortCriteria => new[]{nameof(AuctionDto.Name), nameof(AuctionDto.ActualPrice) };
        
        public SelectList AllSortCriteria => new SelectList(SortCriteria);
        public StaticPagedList<AuctionDto> Auctions { get; set; }
        
        public AuctionFilterDto Filter { get; set; }

        public bool IsPrivate { get; set; } = false;
        public AuctionListModel(IEnumerable<AuctionDto> auctions, int page, int size, int total)
        {
            Auctions = new StaticPagedList<AuctionDto>(auctions, page, size, total);
        }

        public AuctionListModel()
        {
            
        }
    }
}