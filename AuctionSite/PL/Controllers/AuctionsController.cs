using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Controllers.Common;
using PL.Models.Auctions;

namespace PL.Controllers
{
    public class AuctionsController : BaseController
    {
<<<<<<< HEAD
        public AuctionFacade auctionFacade { get; set; }
        public ModifyAuctionsFacade modifyAuctionFacade { get; set; }

=======
        private const string FilterSessionKey = "filter";
        
        private const string SearchSessionKey = "search";
        
        public const int PageSize = 15;
        public AuctionFacade AuctionFacade { get; set; }
        public ModifyAuctionsFacade ModifyAuctionFacade { get; set; }
        
        public ItemFacade ItemFacade { get; set; }
>>>>>>> origin/marek-branch


        public async Task<ActionResult> Index(int page = 1)
        {
            var filter = Session[FilterSessionKey] as AuctionFilterDto ?? new AuctionFilterDto
            {
                ActualDateTime = DateTime.Now
            };
            
            filter.RequestedPageNumber = page;
            filter.PageSize = PageSize;
            var all = await AuctionFacade.GetFilteredAuctionsAsync(filter);
            return View("AuctionList", new AuctionListModel(all.Items, page, PageSize, (int)all.TotalItemsCount));
        }
        
        [HttpPost]
        public async Task<ActionResult> Search(string searchedText)
        {
            var filter = new AuctionFilterDto
            {
                ActualDateTime = DateTime.Now,
                AuctionSearchedName = searchedText,
                PageSize = PageSize,
                RequestedPageNumber = 1
            };
            
            var all = await AuctionFacade.GetFilteredAuctionsAsync(filter);
            return View("AuctionList", new AuctionListModel(all.Items, 1, PageSize, (int)all.TotalItemsCount));
        }
       
        
        [HttpPost]
        public async Task<ActionResult> Index(AuctionListModel model)
        {
            model.Filter.PageSize = PageSize;
            model.Filter.RequestedPageNumber = 1;
            model.Filter.ActualDateTime = model.Filter.IncludeEnded ? DateTime.MinValue : DateTime.Now;
            
            Session[FilterSessionKey] = model.Filter;
            var all = await AuctionFacade.GetFilteredAuctionsAsync(model.Filter);
            return View("AuctionList", new AuctionListModel(all.Items, 1, PageSize, (int)all.TotalItemsCount));
        }
        
        public async Task<ActionResult> ShowItems(int auctionId)
        {
            var items = await ItemFacade.GetItemsAssignedToAuction(auctionId);
            return View("ItemList", items);
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

        public ActionResult ClearFilter()
        {
            Session[FilterSessionKey] = null;
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddBid(int auctionId, AuctionDto dto)
        {
           var auctionDto = await AuctionFacade.GetAuctionById(auctionId);

            if (auctionDto.UserId == UserId)
            {
                TempData["ErrorMessage"] = "Can't bid to the own auction";
                return RedirectToAction("Auction", new {id = auctionId});
            }

            dto.NewRaise = CurrencyController.CalcCurrencyAndGetSymbol(dto.NewRaise, true).Item1;
            
           if (dto.NewRaise <= auctionDto.ActualPrice)
           {
              TempData["ErrorMessage"] = "Raise have to be bigger than actual price";
              return RedirectToAction("Auction", new {id = auctionId});
           }
           
           var raiseDto = new RaiseDto
           {
               Amount = dto.NewRaise, 
               DateTime = DateTime.Now,
               AuctionId = auctionId,
               UserId = HttpContext.User.Identity.GetUserId<int>()
            };
           
            //await ModifyAuctionFacade.AddRaiseToAuctionAsync(raiseDto);
            await ModifyAuctionFacade.AddRaiseToAuctionAsync(raiseDto);
            
            TempData["SuccessMessage"] = "Success";
            return RedirectToAction("Auction", new {id = auctionId});
        }
        

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Delete (int auctionId)
        {
            await AuctionFacade.DeleteAuctionAsync(auctionId);
            return await Index();
        }
    }
}