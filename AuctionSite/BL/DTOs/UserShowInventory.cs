using System.Collections.Generic;
using BL.DTOs.Base;

///////////////////////////////////////
/// Kdyz user zobrazi svuj inventar ///
///////////////////////////////////////

namespace BL.DTOs
{
    public class UserShowInventory
    {
        public string Name { get; set; }
        
        public List<ItemBasicInfo> Items { get; set; }
    }
}