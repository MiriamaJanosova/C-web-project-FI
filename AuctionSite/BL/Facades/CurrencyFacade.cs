using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.Facades.Base;
using BL.Services.Auctions;
using BL.Services.Categories;
using BL.Services.Currencies;
using BL.Services.Items;
using BL.Services.Raises;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class CurrencyFacade: FacadeBase
    {
        private readonly ICurrencyService currencyService;

        public CurrencyFacade(IUnitOfWorkProvider unitOfWorkProvider,
            ICurrencyService currencyService)
            : base(unitOfWorkProvider)
        {
            this.currencyService = currencyService;
        }
        
        public async Task<CurrencyDto> GetCurrencyByCode(string code)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await currencyService.GetActualCurrencyExchangeByCode(code);
            }
        }

        public async Task<bool> UpdateAllCurrencies()
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (!await currencyService.UpdateAllCurrencies())
                {
                    return false;
                }

                await uow.Commit();
            }

            return true;
        }

        public async Task<IEnumerable<CurrencyDto>> GetAllCurrencies()
        {
            using (UnitOfWorkProvider.Create())
            {
                var all = await currencyService.ListAllAsync();
                return all.Items;
            }
        }
    }
}