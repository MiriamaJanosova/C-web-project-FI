using System;
using BL.DTOs.Common;

namespace BL.DTOs.Base
{
    public class RaiseDto : DtoBase
    {
        public double Amount { get; set; }

        public int UserId { get; set; }

        public UserDto User { get; set; }

        public int AuctionId { get; set; }

        public AuctionDto Auction{ get; set; }

        public DateTime DateTime { get; set; }

        public override string ToString()
        {
            return $"{User.UserName} is willing to pay {Amount} for auction {AuctionId} {Auction.Name}";
        }

        protected bool Equals(RaiseDto other)
        {
            if (Id == other.Id)
            {
                return true;
            }
            return Amount == other.Amount &&
                UserId == other.UserId &&
                User.Equals(other.User) &&
                Auction.Equals(other.Auction) &&
                DateTime.Equals(other.DateTime);
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
                Equals((RaiseDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ Amount.GetHashCode();
                hashCode = (hashCode * 397) ^ UserId.GetHashCode();
                hashCode = (hashCode * 397) ^ AuctionId.GetHashCode();
                hashCode = (hashCode * 397) ^ DateTime.GetHashCode();
                return hashCode;
            }
        }
    }
}
