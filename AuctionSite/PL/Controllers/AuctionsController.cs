using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL.Services.Auctions;
using System.Threading.Tasks;
using BL.Facades;
using PL.Controllers.Common;

namespace PL.Controllers
{
    public class AuctionsController : BaseController
    {
        public AuctionFacade AuctionFacade { get; set; }


        public async Task<ActionResult> Index()
        {
            var all = await AuctionFacade.GetAuctionsAsync();
            return View("AuctionList", all);
        }

        public async Task<ActionResult> Auction(int id)
        {
            var dto = await AuctionFacade.GetAuctionById(id);
            if (dto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "One man’s crappy software is another man’s full time job.");
            }

            return View(dto);
        }

        public async Task<ActionResult> Delete(int id)
        {
            // TODO
            return Denied();
        }
    }
}