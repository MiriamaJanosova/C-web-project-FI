using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL.Services.Auctions;
using System.Threading.Tasks;
using BL.DTOs.Auction;
using BL.DTOs.Base;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Controllers.Common;
using PL.Models.Auctions;

namespace PL.Controllers
{
    public class AuctionsController : BaseController
    {
        public AuctionFacade auctionFacade { get; set; }
        public ModifyAuctionsFacade modifyAuctionFacade { get; set; }

        public int UserId => User.Identity.GetUserId<int>(); 


        public async Task<ActionResult> Index()
        {
            var all = await modifyAuctionFacade.GetAllAuctionsAsync();
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
        public async Task<ActionResult> Create()
        {
            var avail = await modifyAuctionFacade.GetAvailableItemsOfUser(UserId);
            return View(new CreateAuctionModel
            {
                AvailableItems = avail.ToList()
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(CreateAuctionModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var dto = model.Dto;

            dto.AuctionerID = User.Identity.GetUserId<int>();
            await AssignItems(dto, model.SelectedItems);

            var res = await modifyAuctionFacade.AddAuctionAsync(dto);
            if (res != 0) // FAILED
            {
                TempData["ErrorMessage"] = "Adding item failed";
                return View();
            }

            return RedirectToAction("MyAuctions", "Account");
        }

        private async Task AssignItems(CreateAuction dto, IList<int> modelSelectedItems)
        {
            foreach (var id in modelSelectedItems)
            {
                var item = await modifyAuctionFacade.GetItem(id);
                dto.AuctionedItems.Add(item);
            }
        }
    }
}