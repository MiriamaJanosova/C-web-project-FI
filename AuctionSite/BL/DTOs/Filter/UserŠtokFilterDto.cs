using BL.DTOs.Common;

namespace BL.DTOs.Filter
{
    public class UserŠtokFilterDto : FilterŠtokDtoBase
    {
        public string UserName { get; set; }
        
        public decimal UserEvaluation { get; set; }
        
    }
}