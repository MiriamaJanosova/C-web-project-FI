using BL.DTOs.Common;
using BL.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Filter
{
    public class CategoryFilterDto : FilterDtoBase
    {
        public List<ItemCategoryType> Names { get; set; }
    }
}
