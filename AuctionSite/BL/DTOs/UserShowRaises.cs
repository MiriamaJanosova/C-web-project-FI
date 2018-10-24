using System.Collections.Generic;

/////////////////////////////////////////
/// Pri zobrazeni vsech prihozu usera ///
/////////////////////////////////////////

namespace BL.DTOs
{
    public class UserShowRaises
    {
        public string Name { get; set; }
        
        public List<RaiseBasicInfo> Raises { get; set; }
    }
}