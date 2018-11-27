using BL.DTOs.Common;
using BL.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Base
{
    public class UserRoleDto : DtoBase
    {
        public int RoleID { get; set; }

        public UserRoleType RoleType { get; set; }

        public int UserID { get; set; }

        public UserDto User { get; set; }

        public override string ToString()
        {
            return $"{UserID} {User.UserName} has Role {RoleType.ToString()}";
        }

        protected bool Equals(UserRoleDto other)
        {
            if (ID == other.ID)
            {
                return true;
            }
            return User.UserName.Equals(other.User.UserName) &&
                UserID == other.UserID &&
                RoleID == other.RoleID &&
                RoleType == other.RoleType;
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
                var hashCode = ID.GetHashCode();
                hashCode = (hashCode * 397) ^ UserID.GetHashCode();
                hashCode = (hashCode * 397) ^ RoleID.GetHashCode();
                hashCode = (hashCode * 397) ^ RoleType.GetHashCode();
                return hashCode;
            }
        }
    }
}
