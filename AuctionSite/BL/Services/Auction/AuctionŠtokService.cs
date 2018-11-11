using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.DTOs.ŠtokDto;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BL.Services.Auction
{
    public class AuctionService : CrudQueryServiceBase<DAL.Entities.Auction, AuctionDto, AuctionFilterDto>, IAuctionService
    {
        public AuctionService(IMapper mapper, 
            IRepository<DAL.Entities.Auction> repository, 
            QueryObjectBase<AuctionDto, 
                DAL.Entities.Auction, 
                AuctionFilterDto, 
                IQuery<DAL.Entities.Auction>> query) 
                : base(mapper, repository, query)
        {
        }

        public int Create(AuctionŠtokDto entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetAuctionŠtokIdsByNamesAsync(params string[] names)
        {
            throw new System.NotImplementedException();
        }

        public Task<AuctionDto> GetŠtokAsync(int entityId)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(AuctionŠtokDto entityDto)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<DAL.Entities.Auction> GetWithIncludesAsync(int entityId)
        {
            throw new System.NotImplementedException();
        }

        Task<QueryResultDto<AuctionŠtokDto, AuctionFilterDto>> IAuctionService.ListAllAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}