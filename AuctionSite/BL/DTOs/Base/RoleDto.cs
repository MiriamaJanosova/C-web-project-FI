using BL.DTOs.Common;
using BL.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Base
{
    public class RoleDto : DtoBase
    {
        public List<UserRoleDto> Users { get; set; }

        public string Name { get; set; }
    }
}
