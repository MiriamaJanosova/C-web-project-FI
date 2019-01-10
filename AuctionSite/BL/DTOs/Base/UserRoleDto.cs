using BL.DTOs.Common;
using BL.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Base
{
    public class UserRoleDto : DtoBase
    {
        public int RoleId { get; set; }
        
        public int UserId { get; set; }

        public UserDto User { get; set; }
        
        public RoleDto Role { get; set; }

        public override string ToString()
        {
            return $"{UserId} {User.UserName}";
        }

        protected bool Equals(UserRoleDto other)
        {
            if (Id == other.Id)
            {
                return true;
            }

            return User.UserName.Equals(other.User.UserName) &&
                   UserId == other.UserId &&
                   RoleId == other.RoleId;
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
                Equals((UserRoleDto)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ UserId.GetHashCode();
                hashCode = (hashCode * 397) ^ RoleId.GetHashCode();
                return hashCode;
            }
        }
    }
}
