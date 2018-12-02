using BL.DTOs.Common;
using System.Collections.Generic;
using BL.DTOs.Base;

namespace BL.DTOs.Filter
{
    public class ItemFilterDto : FilterDtoBase
    {
        public List<ItemCategoryDto> ItemCategoryTypes { get; set; }

        public string SearchedName { get; set; }

        public int OwnerID { get; set; }

        public int AuctionID { get; set; }

    }
}