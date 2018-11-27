using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Common;

namespace BL.DTOs.Filter
{
    public class ItemCategoryFilterDto : FilterDtoBase
    {
        public int ItemID { get; set; }

        public int CategoryID { get; set; }
    }
}
