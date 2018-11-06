using BL.DTOs.Common;

namespace BL.DTOs.Filter
{
    public class ReviewŠtokFilter : FilterŠtokDtoBase
    {
        public decimal From { get; set; }

        public decimal To { get; set; }
    }
}