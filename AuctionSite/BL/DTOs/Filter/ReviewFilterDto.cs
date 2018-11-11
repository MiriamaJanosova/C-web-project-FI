using BL.DTOs.Common;

namespace BL.DTOs.Filter
{
    public class ReviewFilterDto : FilterDtoBase
    {
        public int[] Evaluation { get; set; }

        public int UserID { get; set; }
    }
}