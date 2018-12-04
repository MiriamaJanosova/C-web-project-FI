using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using BL.Services.Common;

namespace BL.Services.Auctions
{
    public interface IAuctionService : IService<AuctionDto, AuctionFilterDto>
    {
        Task<IEnumerable<AuctionDto>> GetAuctionsByNameAsync(string names);

        Task<AuctionDto> GetAsync(int entityId, bool withIncludes = true);

        int Create(AuctionDto entity);

        Task Update(AuctionDto entityDto);

        void Delete(int entityId);

        Task<bool> RaiseForAuction(RaiseDto raiseDto);

        Task<IEnumerable<AuctionDto>> GetCurrentAuctions(DateTime dateTime);

        Task<QueryResultDto<AuctionDto, AuctionFilterDto>> ListFilteredAuctions(AuctionFilterDto filter);

        Task<IEnumerable<AuctionDto>> GetAuctionsForAuctioner(int auctionerID);

        Task<IEnumerable<ItemDto>> GetItemsForAuctionAsync(int auctionsId);

        Task<QueryResultDto<AuctionDto, AuctionFilterDto>> GetAuctionsWithPriceRange(int minPrice, int maxPrice);


    }
}