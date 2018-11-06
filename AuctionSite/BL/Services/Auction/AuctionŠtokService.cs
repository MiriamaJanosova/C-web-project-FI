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
    public class AuctionŠtokService : CrudQueryServiceBase<DAL.Entities.Auction, AuctionŠtokDto, AuctionŠtokFilterDto>, IAuctionŠtokService
    
    {
        public AuctionŠtokService(IMapper mapper, IRepository<DAL.Entities.Auction> repository, QueryŠtokObjectBase<AuctionInfoDto, DAL.Entities.Auction, AuctionŠtokFilterDto, IQuery<DAL.Entities.Auction>> queryŠtok) : base(mapper, repository, queryŠtok)
        {
        }


        public Task<int> GetAuctionŠtokIdsByNamesAsync(params string[] names)
        {
            throw new System.NotImplementedException();
        }

        public Task<AuctionInfoDto> GetŠtokAsync(int entityId)
        {
            throw new System.NotImplementedException();
        }

        protected override Task<DAL.Entities.Auction> GetWithIncludesAsync(int entityId)
        {
            throw new System.NotImplementedException();
        }
    }
}