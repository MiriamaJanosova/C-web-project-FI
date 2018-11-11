using System.Collections.Generic;
using BL.DTOs.Base;

/////////////////////////////////////////
/// Pri zobrazeni vsech prihozu usera ///
/////////////////////////////////////////

namespace BL.DTOs
{
    public class UserShowRaises
    {
        public string Name { get; set; }
        
        public List<RaiseDto> Raises { get; set; }
    }
}