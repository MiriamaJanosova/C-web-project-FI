using BL.DTOs.Common;
using System.Collections.Generic;
using System.Linq;

namespace BL.DTOs.Base
{   
    public class UserDto : DtoBase
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public List<RaiseDto> UserRaisesForAuction { get; set; }

        public List<UserRoleDto> UserRoles { get; set; }

        public List<ItemDto> Inventory { get; set; }

        public List<ReviewDto> Reviews { get; set; }

        public List<AuctionDto> AuctionsCreated { get; set; }

        public decimal ReviewAvg
        {
            get
            {
                var count = Reviews.Count;
                if (count == 0)
                    return 0;
                var total = Reviews.Sum(r => r.Evaluation);
                return total / count;
            }
        }


        public override string ToString()
        {
            return $"{Name} {Surname}";
        }

        protected bool Equals(UserDto other)
        {
            return ID == other.ID;
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
                Equals((UserDto) obj);
        }

        public override int GetHashCode()
        {
            if (ID >= 0)
            {
                return ID.GetHashCode();
            }
            unchecked
            {
                var hashCode = ID.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Surname.GetHashCode();
                hashCode = (hashCode * 397) ^ Email.GetHashCode();
                hashCode = (hashCode * 397) ^ UserRaisesForAuction.GetHashCode();
                hashCode = (hashCode * 397) ^ UserRoles.GetHashCode();
                hashCode = (hashCode * 397) ^ Inventory.GetHashCode();
                hashCode = (hashCode * 397) ^ Reviews.GetHashCode();
                hashCode = (hashCode * 397) ^ AuctionsCreated.GetHashCode();
                return hashCode;
            }
        }
    }
}