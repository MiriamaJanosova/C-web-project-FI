using System;
using BL.DTOs.Common;

namespace BL.DTOs.Base
{
    public class RaiseDto : DtoBase
    {
        public double Amount { get; set; }

        public int UserWhoRaisedID { get; set; }

        public UserDto UserWhoRaised { get; set; }

        public int RaiseForAuctionID { get; set; }

        public AuctionDto RaiseForAuction{ get; set; }

        public DateTime DateTime { get; set; }

        public override string ToString()
        {
            return $"{UserWhoRaised.UserName} is willing to pay {Amount} for auction {RaiseForAuctionID} {RaiseForAuction.Name}";
        }

        protected bool Equals(RaiseDto other)
        {
            if (Id == other.Id)
            {
                return true;
            }
            return Amount == other.Amount &&
                UserWhoRaisedID == other.UserWhoRaisedID &&
                UserWhoRaised.Equals(other.UserWhoRaised) &&
                RaiseForAuction.Equals(other.RaiseForAuction) &&
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
                hashCode = (hashCode * 397) ^ UserWhoRaisedID.GetHashCode();
                hashCode = (hashCode * 397) ^ RaiseForAuctionID.GetHashCode();
                hashCode = (hashCode * 397) ^ DateTime.GetHashCode();
                return hashCode;
            }
        }
    }
}
