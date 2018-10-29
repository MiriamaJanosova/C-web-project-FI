using System.Collections.Generic;
using BL.DTOs.Base;

///////////////////////////////////////////
/// Pri zobrazeni vsech userovych aukci ///
///////////////////////////////////////////

namespace BL.DTOs
{
    public class UserShowAuctions
    {
        public string Name { get; set; }
        
        public List<AuctionBasicInfo> Auctions { get; set; }
    }
}