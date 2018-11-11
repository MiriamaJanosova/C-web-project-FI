using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.DTOs.ŠtokDto;
using BL.QueryObjects.Common;

namespace BL.Services.Auction
{
    public interface IAuctionService
    {
        Task<int> GetAuctionŠtokIdsByNamesAsync(params string[] names);

        Task<AuctionDto> GetŠtokAsync(int entityId);

        int Create(AuctionŠtokDto entity);
        
        Task Update(AuctionŠtokDto entityDto);

        void Delete(int entityId);

        Task<QueryResultDto<AuctionŠtokDto, AuctionFilterDto>> ListAllAsync();
    }
}