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
        public AuctionFacade auctionFacade { get; set; }
        public ModifyAuctionsFacade modifyAuctionFacade { get; set; }


        public async Task<ActionResult> Index()
        {
            var all = await modifyAuctionFacade.GetAllAuctionsAsync();
            return View("AuctionList", all.Items);
        }

        public async Task<ActionResult> Auction(int id)
        {
            var dto = await auctionFacade.GetAuctionById(id);
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

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
    }
}