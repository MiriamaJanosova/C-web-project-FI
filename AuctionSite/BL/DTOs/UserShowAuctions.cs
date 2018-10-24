using System.Collections.Generic;

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