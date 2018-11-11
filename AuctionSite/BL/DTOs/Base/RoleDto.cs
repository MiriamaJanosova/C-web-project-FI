using BL.DTOs.Common;
using BL.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Base
{
    public class RoleDto : DtoBase
    {
        public UserRoleType RoleType { get; set; }

        public List<UserRoleDto> UsersOfRole { get; set; }

        public override string ToString() => RoleType.ToString();     
    }
}
