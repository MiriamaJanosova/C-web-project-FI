using BL.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;
using BL.DTOs.Base;
using DAL.Entities;
using ItemCategoryType = BL.DTOs.Enums.ItemCategoryType;

namespace BL.DTOs.Filter
{
    public class CategoryFilterDto : FilterDtoBase
    {
        public List<ItemCategoryType> Names { get; set; }

    }
}
