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
        Task<AuctionDto> GetAuctionByNameAsync(string names);

        Task<AuctionDto> GetAsync(int entityId, bool withIncludes = true);

        int Create(AuctionDto entity);
        
        Task Update(AuctionDto entityDto);

        void Delete(int entityId);

        Task<int> RaiseForAuction(int raiseID);

        Task<IEnumerable<AuctionDto>> GetCurrentAuctions(DateTime dateTime);


    }
}