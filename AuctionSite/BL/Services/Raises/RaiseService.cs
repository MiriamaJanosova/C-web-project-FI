using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BL.Services.Raises
{
    public class RaiseService :
        CrudQueryServiceBase<Raise, RaiseDto, RaiseFilterDto>,
        IRaiseService
    {
        public RaiseService(IMapper mapper, IRepository<Raise> repository,
            QueryObjectBase<RaiseDto, Raise, RaiseFilterDto, IQuery<Raise>> query)
            : base(mapper, repository, query)
        {
        }

        protected override async Task<Raise> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<QueryResultDto<RaiseDto, RaiseFilterDto>> GetRaisesByAuctionIDAsync(int auctionID)
        {
            return await Query.ExecuteQuery(new RaiseFilterDto() { RaiseForAuctionID = auctionID });
        }

        public async Task<QueryResultDto<RaiseDto, RaiseFilterDto>> GetRaisesByAuctionIDFromOldest(int auctionID)
        {
            return await Query.ExecuteQuery(new RaiseFilterDto() { RaiseForAuctionID = auctionID, SortAscending = true });


        }

        public async Task<QueryResultDto<RaiseDto, RaiseFilterDto>> GetRaisesByUserIDAsync(int userID)
        {
            return await Query.ExecuteQuery(new RaiseFilterDto() { AuctionerID = userID });
        }

        public async Task<QueryResultDto<RaiseDto, RaiseFilterDto>> GetUserRaisesForAuction(int userID, int auctionID)
        {
            return await Query.ExecuteQuery(new RaiseFilterDto { AuctionerID = userID, RaiseForAuctionID = auctionID });

        }

        public async Task<IEnumerable<RaiseDto>> GetRaisesByPriceAsync(double price)
        {
            var queryResult = await Query.ExecuteQuery(new RaiseFilterDto() { Amount = price });
            return queryResult.Items;
        }
    }
}
