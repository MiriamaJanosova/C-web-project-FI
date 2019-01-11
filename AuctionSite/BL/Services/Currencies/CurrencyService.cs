using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using Castle.Core.Internal;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BL.Services.Currencies
{
    public class CurrencyService : CrudQueryServiceBase<Currency, CurrencyDto, CurrencyFilterDto>,
        ICurrencyService
    {
        private readonly IRepository<Currency> currencyRepository;
        private readonly string BASE_URI = "http://free.currencyconverterapi.com";
        private readonly string API_VERSION = "v6";
        private readonly string[] CURRENCIES = {"USD", "EUR", "CZK", "GBP"};

        public CurrencyService(IMapper mapper, IRepository<Currency> repository,
            QueryObjectBase<CurrencyDto, Currency, CurrencyFilterDto, IQuery<Currency>> query)
            : base(mapper, repository, query)
        {
            this.currencyRepository = repository;

        }

        protected override async Task<Currency> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<bool> UpdateAllCurrencies()
        {
            var allCurrencies = await ListAllAsync();
            if (allCurrencies.Items.IsNullOrEmpty())
            {
                return false;
            }

            foreach (var currency in allCurrencies.Items)
            {
                var code = $"USD_{currency.Code}";
                var newRate = FetchSerializedData(code);
                currency.ExchangeRate = newRate;
                var currencyBase = new Currency();
                Repository.Update(ConvertFromTo(currency, currencyBase));
            }

            return false;
        }

        public async Task<bool> CreateDefaultCurrencies()
        {
            var allCurrencies = await ListAllAsync();
            if (allCurrencies.Items.IsNullOrEmpty())
            {
                return false;
            }

            foreach (var currencyCode in CURRENCIES)
            {
                var code = $"USD_{currencyCode}";
                var newRate = FetchSerializedData(code);
                Repository.Create(new Currency {Code = currencyCode, ExchangeRate = newRate});
            }

            return true;
        }

        public async Task<CurrencyDto> GetActualCurrencyExchangeByCode(string code)
        {
            var result = await Query.ExecuteQuery(new CurrencyFilterDto {CodeName = code});
            return result.Items.First();
        }

        private Decimal FetchSerializedData(String code)
        {
            var url = $"{BASE_URI}/api/{API_VERSION}/convert?q={code}&compact=y";
            var webClient = new WebClient();
            string jsonData;

            var conversionRate = 1.0m;
            try
            {
                jsonData = webClient.DownloadString(url);
                var jsonObject =
                    new JavaScriptSerializer().Deserialize<Dictionary<string, Dictionary<string, decimal>>>(jsonData);
                var result = jsonObject[code];
                conversionRate = result["val"];

            }
            catch (Exception)
            {
            }

            return conversionRate;
        }
    }
}