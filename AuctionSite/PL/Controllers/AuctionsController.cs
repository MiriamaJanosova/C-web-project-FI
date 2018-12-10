using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL.Services.Auctions;
using System.Threading.Tasks;
using BL.DTOs.Auction;
using BL.DTOs.Base;
using BL.Facades;
using DAL.Entities;
using Microsoft.AspNet.Identity;
using PL.Controllers.Common;
using PL.Models.Auctions;

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
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddBid(int auctionId, AuctionDto dto)
        {
           var auctionDto = await auctionFacade.GetAuctionById(auctionId);

            if (auctionDto.AuctionerID == UserId)
            {
                TempData["ErrorMessage"] = "Can't bid to the own auction";
            }
            if (dto.NewRaise <= auctionDto.TestPrice)
            {
               TempData["ErrorMessage"] = "Raise have to be bigger than actual price";
               return RedirectToAction("Auction", new {id = auctionId});
            }
            
            var raiseDto = new RaiseDto
            {
                Amount = dto.NewRaise, 
                DateTime = DateTime.Now,
                RaiseForAuctionID = auctionId,
                UserWhoRaisedID = HttpContext.User.Identity.GetUserId<int>()
            };

            await modifyAuctionFacade.AddRaiseAsync(raiseDto);
            TempData["ErrorMessage"] = "Success";
            return RedirectToAction("Auction", new {id = auctionId});
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
            dto.ImageBytes = await ImageToByteArray(dto.Upload.InputStream);
            dto.AuctionerID = User.Identity.GetUserId<int>();
            var res = await modifyAuctionFacade.AddAuctionAsync(dto);
            if (res == 0) // FAILED
            {
                TempData["ErrorMessage"] = "Adding item failed";
                return View();
            }

            await AssignAuctionToItems(res, model.SelectedItems);
            return RedirectToAction("MyAuctions", "Account");
        }

        private async Task AssignAuctionToItems(int auctionId, IList<int> itemIds)
        {
            foreach (var id in itemIds)
            {
                var item = await modifyAuctionFacade.GetItem(id);
                item.AuctionID = auctionId;
                await modifyAuctionFacade.UpdateItem(item);
            }
        }
        
        private async Task<byte[]> ImageToByteArray(Stream input)
        {
            var ms = new MemoryStream();
            await input.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}