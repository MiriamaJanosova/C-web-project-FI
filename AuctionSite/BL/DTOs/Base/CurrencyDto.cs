using BL.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs.Base
{
    public class CurrencyDto : DtoBase
    {
        public string Code { get; set; }

        public double ExchangeRate { get; set; }

        public override string ToString() => Code;

    }
}
