using System.Collections.Generic;

/////////////////////////////////////////////////////////
/// Kdyz user zobrazi vsechna hodnoceni, ktere napsal ///
/////////////////////////////////////////////////////////


namespace BL.DTOs
{
    public class UserShowReviewsFromHim
    {
        public string Name { get; set; }
        
        public List<ReviewFromUser> Reviews { get; set; }
        
    }
}