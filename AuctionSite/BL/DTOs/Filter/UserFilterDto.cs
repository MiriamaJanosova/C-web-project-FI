using BL.DTOs.Common;

namespace BL.DTOs.Filter
{
    public class UserFilterDto : FilterDtoBase
    {
        public string UserEmail { get; set; }
        
        public int[] UserEvaluation { get; set; }        
    }
}