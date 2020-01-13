using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.Services.Common;
using DAL.Entities;

namespace BL.Services.Currencies
{
    public interface ICurrencyService :  IService<CurrencyDto, CurrencyFilterDto>
    {
        Task<CurrencyDto> GetAsync(int entityId, bool withIncludes = true);

        Currency Create(CurrencyDto entity);

        Task Update(CurrencyDto entityDto);

        void Delete(int entityId);
        Task<CurrencyDto> GetActualCurrencyExchangeByCode(string code);

        Task<bool> CreateDefaultCurrencies();

        Task<bool> UpdateAllCurrencies();
    }
}