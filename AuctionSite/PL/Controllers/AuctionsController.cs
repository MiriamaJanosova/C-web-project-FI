using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL.Services.Auctions;
using System.Threading.Tasks;
using BL.DTOs.Auction;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Controllers.Common;

namespace PL.Controllers
{
    public class AuctionsController : BaseController
    {
        public AuctionFacade auctionFacade { get; set; }
        public ModifyAuctionsFacade modifyAuctionFacade { get; set; }


        public async Task<ActionResult> Index()
        {
            var all = await auctionFacade.GetAllAuctionsAsync();
            return View("AuctionList", all);
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

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(CreateAuction model)
        {
            if (!ModelState.IsValid)
                return View();

            model.AuctionerID = User.Identity.GetUserId<int>();
            var res = await modifyAuctionFacade.AddAuctionAsync(model);
            if (res == 0) // FAILED
            {
                TempData["ErrorMessage"] = "Adding item failed";
                return View();
            }

            return RedirectToAction("MyAuctions", "Account");
        }
    }
}