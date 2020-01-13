using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using BL.DTOs.Base;
using BL.Facades;
using PL.Controllers.Common;
using PL.Models.Currency;

namespace PL.Controllers
{
    
    [Authorize(Roles = "Admin")]
    public class CurrencyController : BaseController
    {
        public static CurrencyFacade CurrencyFacade { get; set; }
        public static string CurrencySession => (string)System.Web.HttpContext.Current.Session["currency"];

        public CurrencyController(CurrencyFacade currencyFacade)
        {
            CurrencyFacade = currencyFacade;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var currencies = await CurrencyFacade.GetAllCurrencies();
            return View("CurrencyList", new CurrencyListModel {Currencies = currencies});
        }

        public async Task<ActionResult> Delete(int id)
        {
            await CurrencyFacade.DeleteCurrency(id);
            return await Index();
        }

        public ActionResult Create()
        {
            return View("Currency");
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(CurrencyDto dto)
        {
            if (await CurrencyFacade.CreateCurrency(dto) == 0)
            {
                TempData["Error"] = "Cant add new currency";
                return View("Currency");
            }

            TempData["Success"] = "Success";
            return await Index();
        }

        public async Task<ActionResult> Edit(int id)
        {
            var currency = await CurrencyFacade.GetCurrencyById(id);
            return currency == null ? Denied() : View("Currency", currency);
        }
        
        [HttpPost]
        public async Task<ActionResult> Edit(CurrencyDto dto)
        {
            await CurrencyFacade.UpdateCurrency(dto);
            return await Index();
        }

        public async Task<ActionResult> Update()
        {
            if (!await CurrencyFacade.UpdateAllCurrencies())
            {
                TempData["Error"] = "failed to update rates";
                return await Index();
            }

            TempData["Success"] = "Update successful";
            return await Index();
        }
        
        public async Task<ActionResult> CreateAll()
        {
            if (!await CurrencyFacade.CreateAllDefaultCurrencies())
            {
                TempData["Error"] = "Failed to create currencies";
                return await Index();
            }

            TempData["Success"] = "Created successful";
            return await Index();
        }

        [AllowAnonymous]
        public async Task<ActionResult> Change(string code)
        {
            var currency = await CurrencyFacade.GetCurrencyByCode(code);
            if (currency == null)
            {
                return await Index();
            }

            Session["currency"] = code;
            return await Index();
        }
        public static Tuple<decimal,string> CalcCurrencyAndGetSymbol(decimal price, bool create = false)
        {
            var currencyLocal = CurrencySession;
            if (currencyLocal == null)
            {
                return Tuple.Create(price, "$");
                
            }

            var currency = Task.Run( async () => await CurrencyFacade.GetCurrencyByCode(currencyLocal));
            currency.Wait();
            if (currency.Result == null)
            {
                return Tuple.Create(price, "$");
            }

            return create ? Tuple.Create(price / currency.Result.ExchangeRate, currency.Result.Symbol) : Tuple.Create(currency.Result.ExchangeRate * price, currency.Result.Symbol);
        }

    }
}