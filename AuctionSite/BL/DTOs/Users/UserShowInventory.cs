using System.Collections.Generic;
using BL.DTOs.Base;

///////////////////////////////////////
/// Kdyz user zobrazi svuj inventar ///
///////////////////////////////////////

namespace BL.DTOs.Users
{
    public class UserShowInventory
    {
        public string Name { get; set; }
        
        public List<ItemDto> Items { get; set; }
    }
}