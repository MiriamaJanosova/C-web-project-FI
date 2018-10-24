using System.Collections.Generic;

//////////////////////////////////////////
// Class pri zobrazovani inventory usera//
//////////////////////////////////////////

namespace BL.DTOs
{
    public class ItemBasicInfo
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public List<string> Categories { get; set; }
    }
}