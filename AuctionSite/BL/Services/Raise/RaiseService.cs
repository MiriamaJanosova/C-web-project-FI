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

        public async Task<RaiseDto> GetRaisesByAuctionIDAsync(int auctionID)
        {
            var queryResult = await Query.ExecuteQuery(new RaiseFilterDto() { RaiseForAuctionID = auctionID });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<RaiseDto> GetRaisesByAuctionerIDAsync(int auctionerID)
        {
            var queryResult = await Query.ExecuteQuery(new RaiseFilterDto() { AuctionerID =  auctionerID});
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<RaiseDto> GetRaisesByPriceAsync(double price)
        {
            var queryResult = await Query.ExecuteQuery(new RaiseFilterDto() { Amount = price });
            return queryResult.Items.SingleOrDefault();
        }


    }
}
