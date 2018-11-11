using BL.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Base
{
    public class EmailTemplateDto : DtoBase 
    {
        public string Message { get; set; }

        public override string ToString() => Message;

    }
}
