using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BL.Services.Auctions
{
    public class AuctionService : 
        CrudQueryServiceBase<Auction, AuctionDto, AuctionFilterDto>, 
        IAuctionService
    {
        public AuctionService(IMapper mapper, IRepository<Auction> repository, 
            QueryObjectBase<AuctionDto, Auction, AuctionFilterDto, IQuery<Auction>> query) 
                : base(mapper, repository, query) {}

        public async Task<AuctionDto> GetAuctionByNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new AuctionFilterDto { AuctionSearchedName = name });
            return queryResult.Items.SingleOrDefault();
        }


        protected async override Task<Auction> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }
    }
}