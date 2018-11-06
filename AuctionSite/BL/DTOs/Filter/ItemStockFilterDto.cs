using BL.DTOs.Common;
using DAL.Entities;

namespace BL.DTOs.Filter
{
    public class ItemStockFilterŠtokDto : FilterŠtokDtoBase
    {
        public string CategoryName { get; set; }

        public string SearchedName { get; set; }
    }
}