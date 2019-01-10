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
        
        public double ActualPrice { get; set; }
        
        public int AuctionerID { get; set; }

        public UserDto Auctioner { get; set; }
        
        public double NewRaise { get; set; }
        
        public List<ImageDto> ImageBytes { get; set; }
        
        public Image Image { get; set; }

        public List<ItemDto> AuctionedItems { get; set; }

        public List<RaiseDto> RaisesForAuction { get; set; }
        
        public double TestPrice 
        {
            get
            {
                if (RaisesForAuction == null || RaisesForAuction.Count == 0)
                {
                    return ActualPrice;
                }
                return RaisesForAuction.Max(x => x.Amount);
            }
        
        }

        public override string ToString()
        {
            return $"Auction set up by {AuctionerID} at: {StartDate}";
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
            return obj.GetType() == this.GetType() &&
                Equals((AuctionDto) obj);
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
                hashCode = (hashCode * 397) ^ AuctionerID.GetHashCode();
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
