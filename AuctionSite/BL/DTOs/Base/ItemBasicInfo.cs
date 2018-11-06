using System.Collections.Generic;
using BL.DTOs.Common;

//////////////////////////////////////////
// Class pri zobrazovani inventory usera//
//////////////////////////////////////////

namespace BL.DTOs.Base
{
    public class ItemBasicInfo : DtoBase
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public List<string> Categories { get; set; }
    }
}