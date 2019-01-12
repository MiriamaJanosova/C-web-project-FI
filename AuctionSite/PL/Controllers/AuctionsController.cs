using System;
using System.Collections.Generic;
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
        public AuctionFacade AuctionFacade { get; set; }
        public ModifyAuctionsFacade ModifyAuctionFacade { get; set; }


        public async Task<ActionResult> Index(int page = 1)
        {
            var all = await AuctionFacade.GetFilteredAuctionsAsync(new AuctionFilterDto { RequestedPageNumber = page, PageSize = 15 });
            return View("AuctionList", new AuctionListModel(all.Items));
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
        
       
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Create()
        {
            var avail = await ModifyAuctionFacade.GetAvailableItemsOfUser(UserId);
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

            foreach (var file in dto.Upload)
            {
                if (file?.InputStream == null)
                {
                    continue;
                }   
                dto.ImageBytes.Add(new ImageDto(await ImageToByteArray(file.InputStream)));
            }
            
            dto.UserId = User.Identity.GetUserId<int>();
            dto.StartPrice = CurrencyController.CalcCurrencyAndGetSymbol(dto.StartPrice, true).Item1;
            dto.ActualPrice = dto.StartPrice;
            var res = await ModifyAuctionFacade.AddAuctionAsync(dto);
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
                var item = await ModifyAuctionFacade.GetItem(id);
                item.AuctionID = auctionId;
                await ModifyAuctionFacade.UpdateItem(item);
            }
        }
        
        private static async Task<byte[]> ImageToByteArray(Stream input)
        {
            var ms = new MemoryStream();
            await input.CopyToAsync(ms);
            return ms.ToArray();
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