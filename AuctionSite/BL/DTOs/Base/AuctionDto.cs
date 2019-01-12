using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using BL.DTOs.Common;

namespace BL.DTOs.Base
{
    public class AuctionDto : DtoBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public decimal ActualPrice { get; set; }
        
        public int UserId { get; set; }

        public UserDto User { get; set; }

        public decimal NewRaise { get; set; }

        public List<ImageDto> ImageBytes { get; set; }
        
        public Image Image { get; set; }

        public List<ItemDto> AuctionedItems { get; set; }

        public List<RaiseDto> RaisesForAuction { get; set; }
        
        public override string ToString()
        {
            return $"Auction set up by {UserId} at: {StartDate}";
        }

        protected bool Equals(AuctionDto other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == this.GetType() && ((AuctionDto)obj).GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            if (Id >= 0)
            {
                return Id.GetHashCode();
            }
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ StartDate.GetHashCode();
                hashCode = (hashCode * 397) ^ EndDate.GetHashCode();
                hashCode = (hashCode * 397) ^ UserId.GetHashCode();
                hashCode = (hashCode * 397) ^ Description.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Convert.ToInt32(ActualPrice);
                hashCode = (hashCode * 397) ^ AuctionedItems.GetHashCode();
                hashCode = (hashCode * 397) ^ RaisesForAuction.GetHashCode();
                return hashCode;
            }
        }
    }
}
