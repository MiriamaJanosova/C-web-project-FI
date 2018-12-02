using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.Facades;

namespace WebAPI.Controllers
{
    public class AuctionsController : Controller
    {
        public AuctionFacade AuctionFacade { get; set; }
        public ModifyAuctionsFacade ModifyAuctionsFacade { get; set; }
        public UserFacade UserFacade { get; set; }
        public ReviewFacade ReviewFacade { get; set; }


        public async Task<IEnumerable<AuctionDto>> GetAuctionsForUser(int userId)
        {
            var user = (await UserFacade.GetUserByIdAsync(userId));
            var auctions = (await AuctionFacade.GetAuctionsForAuctioner(user));
            return auctions;

        }
        
        public async Task<IEnumerable<AuctionDto>> GetAuctionQuery(string sort = null, bool asc = true,
            string name = null, int minPrice = 0, int maxPrice = int.MaxValue,
            [FromUri] CategoryDto[] categories = null)
        {
            var filter = new AuctionFilterDto()
            {
                SortCriteria = sort,
                SortAscending = asc,
                AuctionSearchedName = name,
                MinimalPrice = minPrice,
                MaximalPrice = maxPrice,
                AuctionerID = 0
            };
            var auctions = (await AuctionFacade.GetFilteredAuctionsAsync(filter)).Items;
            foreach (var auction in auctions)
            {
                auction.ID = 0;
            }
            return auctions;
        }

        public async Task<IEnumerable<AuctionDto>> GetCurrentAuctions(int id)
        {
            var auctions = (await AuctionFacade.GetCurrentAuctionsAsync(DateTime.Now));
            foreach (var auction in auctions)
            {
                auction.ID = 0;
            }
            return auctions;
        }

        public async Task<IEnumerable<ItemDto>> GetItemsForAuction(int id)
        {
            var auction = await AuctionFacade.GetAuctionById(id);
            if (auction == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var items = await AuctionFacade.GetItemsForAuction(auction);
            foreach (var item in items)
            {
                item.ID = 0;
            }
            return items;
        }

        public async Task<IEnumerable<RaiseDto>> GetRaisesForAuction(int id)
        {
            var auction = await AuctionFacade.GetAuctionById(id);
            if (auction == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var raises = await AuctionFacade.GetRaisesForAuction(auction);
            foreach (var raise in raises)
            {
                raise.ID = 0;
            }
            return raises;
        }

        public async Task<string> DeleteAuction(int id)
        {
            var success = await ModifyAuctionsFacade.DeleteAuctionAsync(id);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Deleted auction with id: {id}";
        }

        public async Task<string> DeleteRaise(int id)
        {
            var success = await ModifyAuctionsFacade.DeleteRaiseAsync(id);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Deleted raise with id: {id}";
        }

        public async Task<string> DeleteItem(int id)
        {
            var success = await ModifyAuctionsFacade.DeleteItemAsync(id);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Deleted item with id: {id}";
        }

        public async Task<string> DeleteItemCategory(int id)
        {
            var success = await ModifyAuctionsFacade.DeleteItemCategory(id);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Deleted itemCategory with id: {id}";
        }

        public async Task<string> PutItemInAuction(int id, [FromBody]ItemDto item)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var success = await ModifyAuctionsFacade.UpdateItemsAsync(item);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Updated item in auction with id: {id}";
        }

        public async Task<string> PutAuction(int id, [FromBody]AuctionDto auction)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var success = await ModifyAuctionsFacade.UpdateAuctionAsync(auction);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Updated auction with id: {id}";
        }

        public async Task<string> PutItemCategory(int id, [FromBody]ItemCategoryDto itemCategory)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var success = await ModifyAuctionsFacade.UpdateItemCategoryAsync(itemCategory);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Updated itemCategory with id: {id}";
        }

        public async Task<string> PutRaise(int id, [FromBody]RaiseDto raise)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var success = await ModifyAuctionsFacade.UpdateRaiseAsync(raise);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Updated raise with id: {id}";
        }

        public async Task<string> PostItemCategory([FromBody]ItemCategoryDto itemCategory)
        {
            if (itemCategory == null || !ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var itemCatId = await ModifyAuctionsFacade.AddItemCategoryAsync(itemCategory);
            if (itemCatId == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return $"Added category {itemCategory.CategoryID} to item {itemCategory.ItemID }.";
        }

       public async Task<string> PostRaise([FromBody]RaiseDto raise)
        {
            if (raise == null || !ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var raiseId = await ModifyAuctionsFacade.AddRaiseAsync(raise);
            if (raiseId == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return $"Created raise with id: {raiseId}.";
        }


        public async Task<string> PostItem(int userId, [FromBody]ItemDto item)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var res = await ModifyAuctionsFacade.AddItems(item);
            if (res == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return $"Created item in auction  with id: {res}.";
        }

        public async Task<string> PostAuction([FromBody]AuctionDto auction)
        {
            if (auction == null || !ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var auctionId = await ModifyAuctionsFacade.AddAuctionAsync(auction);
            if (auctionId == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return $"Created auction with id: {auctionId}.";
        }
    }
}
