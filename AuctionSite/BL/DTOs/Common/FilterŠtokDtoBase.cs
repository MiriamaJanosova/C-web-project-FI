namespace BL.DTOs.Common
{
    public class FilterŠtokDtoBase
    {
        public int? RequestedPageNumber { get; set; }
        
        public int PageSize { get; set; }
        
        public string SortCriteria { get; set; }
        
        public bool SortAscending { get; set; }
    }
}