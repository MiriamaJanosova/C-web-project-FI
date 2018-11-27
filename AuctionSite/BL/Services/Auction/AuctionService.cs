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

        public async Task<AuctionDto> GetAuctionByNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new AuctionFilterDto { AuctionSearchedName = name });
            return queryResult.Items.SingleOrDefault();
        }


        protected override async Task<Auction> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<IEnumerable<AuctionDto>> GetCurrentAuctions(DateTime dateTime)
        {
            var queryResult = await Query.ExecuteQuery(new AuctionFilterDto() {ActualDateTime = dateTime});
            return queryResult.Items;
        }

        public async Task<int> RaiseForAuction(int raiseID)
        {
            var raise = await raiseRepository.GetAsync(raiseID);

            var user = await userRepository.GetAsync(raise.UserWhoRaisedID);
            if (user == null)
                throw new ArgumentException("Raise amount must be bigger than actual price");

            var auction = await Repository.GetAsync(raise.RaiseForAuctionID);
            if (auction.ActualPrice >= raise.Amount)
                throw new ArgumentException("Raise amount must be bigger than actual price");
            if (auction.StartDate.CompareTo(raise.DateTime) > 0 || auction.EndDate.CompareTo(raise.DateTime) <= 0)
                throw new ArgumentException("you cannot raiseDto for this auction, it hasn't started yet or has already finished. ");

            auction.ActualPrice = raise.Amount;
            auction.RaisesForAuction.Add(raise);

            Repository.Update(auction);
            return auction.Id;
        }
    }
}