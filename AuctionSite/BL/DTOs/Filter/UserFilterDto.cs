using BL.DTOs.Common;

namespace BL.DTOs.Filter
{
    public class UserFilterDto : FilterDtoBase
    {
        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public int ID { get; set; }
    }
}