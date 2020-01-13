using System.Collections.Generic;
using BL.DTOs.Base;
using X.PagedList;

namespace PL.Models.Raise
{
    public class RaisesListViewModel
    {
        public StaticPagedList<RaiseDto> Raises { get; set; }
        
        public RaisesListViewModel(IEnumerable<RaiseDto> auctions, int page, int size, int total)
        {
            Raises = new StaticPagedList<RaiseDto>(auctions, page, size, total);
        }

        public RaisesListViewModel()
        {
            
        }
    }
}