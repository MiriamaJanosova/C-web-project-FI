using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.Facades.Base;
using BL.QueryObjects.Common;
using BL.Services.Auctions;
using BL.Services.Categories;
using BL.Services.Items;
using BL.Services.Raises;
using BL.Services.Reviews;
using DAL.Entities;
using Infrastructure.UnitOfWork;
namespace BL.Facades
{
    public class AuctionFacade : FacadeBase
    {
        private readonly IAuctionService auctionService;
        private readonly IRaiseService raiseService;
        private readonly IItemService itemService;
        private readonly IItemCategoryService itemCategoryService;
        private readonly ICategoryService categoryService;

        public AuctionFacade(IUnitOfWorkProvider unitOfWorkProvider, IAuctionService auctionService,
            IRaiseService raiseService)
            : base(unitOfWorkProvider)
        {
            this.auctionService = auctionService;
            this.raiseService = raiseService;
        }

        public async Task<AuctionDto> GetAuctionById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await auctionService.GetAsync(id);
            }
        }

        public async Task<IEnumerable<RaiseDto>> GetRaisesForAuction(AuctionDto auction)
        {
            if (auction == null)
            {
                return new List<RaiseDto>();
            }

            var raises = await raiseService.GetRaisesByAuctionIDAsync(auction.Id);
            return raises.Items;
        }

        public async Task<IEnumerable<RaiseDto>> GetRaisesForAuctionFromOldest(AuctionDto auction)
        {
            if (auction == null)
            {
                return new List<RaiseDto>();
            }

            var raises = await raiseService.GetRaisesByAuctionIDFromOldest(auction.Id);
            return raises.Items;
        }

        public async Task<IEnumerable<AuctionDto>> GetCurrentAuctionsAsync(DateTime now)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await auctionService.GetCurrentAuctions(now);
            }
        }

        public async Task<QueryResultDto<AuctionDto, AuctionFilterDto>> GetAuctionsWithPrices(int minPrice, int maxPrice)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await auctionService.GetAuctionsWithPriceRange(minPrice, maxPrice);
            }
        }

        public async Task<QueryResultDto<AuctionDto, AuctionFilterDto>> GetFilteredAuctionsAsync(AuctionFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                if (filter == null)
                {
                    return await auctionService.ListAllAsync();
                }

                return await auctionService.ListFilteredAuctions(filter);
            }
        }

        public async Task<IEnumerable<AuctionDto>> GetAuctionsForAuctioner(UserDto user)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await auctionService.GetAuctionsForAuctioner(user.Id);
            }
        }

        public async Task<IEnumerable<ItemDto>> GetItemsForAuction(AuctionDto auction)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await auctionService.GetItemsForAuctionAsync(auction.Id);
            }
        }

        public async Task<IEnumerable<AuctionDto>> GetAuctionsByName(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await auctionService.GetAuctionsByNameAsync(name);
            }
        }

        public async Task<bool> RaiseForAuction(RaiseDto raise)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await auctionService.RaiseForAuction(raise);
            }
        }

    }
}
