using System.Collections.Generic;
using BL.DTOs.Base;

namespace PL.Models.Currency
{
    public class CurrencyListModel
    {
        public IEnumerable<CurrencyDto> Currencies { get; set; }

    }
}