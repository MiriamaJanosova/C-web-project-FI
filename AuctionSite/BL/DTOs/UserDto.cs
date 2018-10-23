using System.Collections.Generic;
using DAL.Entities;

namespace BL.DTOs
{
    public class UserDto
    {
        public string Name { get; set; }
        
        public List<Raise> UserRaisesForAuction { get; set; }
        
        public List<UserRole> UserRoles { get; set; }
        
        public List<Item> Inventory { get; set; }
        
        public List<Auction> AuctionsCreated { get; set; }
    }
}