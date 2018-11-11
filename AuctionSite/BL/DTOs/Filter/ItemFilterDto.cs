using BL.DTOs.Common;
using DAL.Entities;
using System.Collections.Generic;

namespace BL.DTOs.Filter
{
    public class ItemFilterDto : FilterDtoBase
    {
        public List<ItemCategoryType> CategoryTypes { get; set; }

        public string SearchedName { get; set; }
    }
}