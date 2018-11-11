using System;
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

        public override string ToString()
        {
            return $"Auction set up by {AuctionerID} at: {StartDate}";
        }

        protected bool Equals(AuctionDto other)
        {
            if (ID == other.ID)
            {
                return true;
            }
            return AuctionerID == other.AuctionerID &&
                StartDate.Equals(other.StartDate) &&
                EndDate.Equals(other.EndDate) &&
                ActualPrice == other.ActualPrice &&
                Name.Equals(other.Name) &&
                Description.Equals(other.Description);
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
            unchecked
            {
                var hashCode = ID.GetHashCode();
                hashCode = (hashCode * 397) ^ StartDate.GetHashCode();
                hashCode = (hashCode * 397) ^ EndDate.GetHashCode();
                hashCode = (hashCode * 397) ^ AuctionerID.GetHashCode();
                hashCode = (hashCode * 397) ^ Description.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ ActualPrice.GetHashCode();
                return hashCode;
            }
        }
    }
}
