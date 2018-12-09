using BL.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Base
{
    public class ReviewDto : DtoBase
    {
        public decimal Evaluation { get; set; }

        public string Description { get; set; }

        public int ReviewedUserID { get; set; }

        public UserDto ReviewedUser { get; set; }

        public int UserWhoWroteID { get; set; }

        public UserDto UserWhoWrote { get; set; }

        public override string ToString()
        {
            return $"{ReviewedUser.UserName} got {Evaluation} points.";
        }

        protected bool Equals(ReviewDto other)
        {
            if (Id == other.Id)
            {
                return true;
            }
            return Evaluation == other.Evaluation &&
                Description.Equals(other.Description) &&
                ReviewedUserID == other.ReviewedUserID &&
                UserWhoWroteID == other.UserWhoWroteID;
                
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
                Equals((ReviewDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ ReviewedUserID.GetHashCode();
                hashCode = (hashCode * 397) ^ Evaluation.GetHashCode();
                hashCode = (hashCode * 397) ^ Description.GetHashCode();
                hashCode = (hashCode * 397) ^ UserWhoWroteID.GetHashCode();
                return hashCode;
            }
        }
    }
}
