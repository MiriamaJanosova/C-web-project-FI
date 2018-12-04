using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;
using Infrastructure.UnitOfWork;

namespace BL.Services.Auctions
{
    public class AuctionService :
        CrudQueryServiceBase<Auction, AuctionDto, AuctionFilterDto>,
        IAuctionService
    {
        private readonly IRepository<Raise> raiseRepository;
        private readonly IRepository<User> userRepository;

        public AuctionService(IMapper mapper, IRepository<Auction> repository,
            IRepository<Raise> raiseRepository, IRepository<User> userRepository,
            QueryObjectBase<AuctionDto, Auction, AuctionFilterDto, IQuery<Auction>> query)
            : base(mapper, repository, query)
        {
            this.raiseRepository = raiseRepository;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<AuctionDto>> GetAuctionsByNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new AuctionFilterDto { AuctionSearchedName = name });
            return queryResult.Items;
        }

        public async Task<IEnumerable<AuctionDto>> GetAuctionsForAuctioner(int auctionerID)
        {
            var queryResult = await Query.ExecuteQuery(new AuctionFilterDto { AuctionerID = auctionerID });
            return queryResult.Items;
        }

        public async Task<IEnumerable<ItemDto>> GetItemsForAuctionAsync(int auctionsId)
        {
            var result = await GetWithIncludesAsync(auctionsId);
            return result.AuctionedItems
                .Select(c => Mapper.Map<ItemDto>(c))
                .ToList();
        }

        protected override async Task<Auction> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<QueryResultDto<AuctionDto, AuctionFilterDto>> GetAuctionsWithPriceRange(int minPrice,
            int maxPrice)
        {
            return await Query.ExecuteQuery(new AuctionFilterDto { MaximalPrice = maxPrice, MinimalPrice = minPrice });
        }

        public async Task<QueryResultDto<AuctionDto, AuctionFilterDto>> ListFilteredAuctions(AuctionFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }

        public async Task<IEnumerable<AuctionDto>> GetCurrentAuctions(DateTime dateTime)
        {
            var queryResult = await Query.ExecuteQuery(new AuctionFilterDto() { ActualDateTime = dateTime });
            return queryResult.Items;
        }

        public async Task<bool> RaiseForAuction(RaiseDto raiseDto)
        {
            if (raiseDto == null) return false;

            var user = await userRepository.GetAsync(raiseDto.UserWhoRaisedID);
            if (user == null)
                return false;

            var auction = await Repository.GetAsync(raiseDto.RaiseForAuctionID);
            if (auction.ActualPrice >= raiseDto.Amount)
                throw new ArgumentException("Raise amount must be bigger than actual price");
            if (auction.StartDate.CompareTo(raiseDto.DateTime) > 0 || auction.EndDate.CompareTo(raiseDto.DateTime) <= 0)
                throw new ArgumentException("you cannot raiseDto for this auction, it hasn't started yet or has already finished. ");

            auction.ActualPrice = raiseDto.Amount;
            var raise = Mapper.Map<Raise>(raiseDto);
            auction.RaisesForAuction.Add(raise);

            Repository.Update(auction);
            return true;
        }

        
    }
}