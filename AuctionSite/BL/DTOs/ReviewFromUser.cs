namespace BL.DTOs
{
    /**nemame ziadne info o tom kto dal review*/
    public class ReviewFromUser
    {
        public int Evaluation { get; set; }
        
        public string Description { get; set; }
        
        public string ReviewedUser { get; set; }
    }
}