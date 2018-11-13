﻿using BL.DTOs.Common;
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

        public override string ToString()
        {
            return $"{ReviewedUser.Name} got {Evaluation} points.";
        }

        protected bool Equals(ReviewDto other)
        {
            if (ID == other.ID)
            {
                return true;
            }
            return Evaluation == other.Evaluation &&
                Description.Equals(other.Description) &&
                ReviewedUserID == other.ReviewedUserID;
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
                var hashCode = ID.GetHashCode();
                hashCode = (hashCode * 397) ^ ReviewedUserID.GetHashCode();
                hashCode = (hashCode * 397) ^ Evaluation.GetHashCode();
                hashCode = (hashCode * 397) ^ Description.GetHashCode();
                return hashCode;
            }
        }
    }
}