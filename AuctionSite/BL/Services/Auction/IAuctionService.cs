using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;

namespace BL.Services.Auctions
{
    public interface IAuctionService
    {
        Task<AuctionDto> GetAuctionByNameAsync(string names);

        Task<AuctionDto> GetAsync(int entityId, bool withIncludes = true);

        int Create(AuctionDto entity);
        
        Task Update(AuctionDto entityDto);

        void Delete(int entityId);

        Task<QueryResultDto<AuctionDto, AuctionFilterDto>> ListAllAsync();
    }
}