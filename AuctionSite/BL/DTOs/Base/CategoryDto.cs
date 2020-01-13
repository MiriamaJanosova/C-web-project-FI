using BL.DTOs.Common;
using BL.DTOs.Enums;
using System.Collections.Generic;

namespace BL.DTOs.Base
{
    public class CategoryDto : DtoBase
    {        
        public string Description { get; set; }

        public string CategoryType { get; set; }

        public List<ItemCategoryDto> ItemsWithCategory { get; set; }

        public override string ToString() => CategoryType.ToString();


    }
}
