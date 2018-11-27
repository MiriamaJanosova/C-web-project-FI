using BL.DTOs.Common;
using DAL.Entities;
using System.Collections.Generic;

namespace BL.DTOs.Filter
{
    public class ItemFilterDto : FilterDtoBase
    {
        public List<ItemCategory> ItemCategoryTypes { get; set; }

        public string SearchedName { get; set; }

        public int OwnerID { get; set; }

        public int AuctionID { get; set; }

    }
}