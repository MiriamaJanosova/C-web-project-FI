using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.Services.Common;

namespace BL.Services.Currencies
{
    public interface ICurrencyService :  IService<CurrencyDto, CurrencyFilterDto>
    {
        Task<CurrencyDto> GetActualCurrencyExchangeByCode(string code);

        Task<bool> CreateDefaultCurrencies();

        Task<bool> UpdateAllCurrencies();
    }
}