using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;

namespace BL.QueryObjects
{
    public class AuctionŠtokQueryObject : QueryŠtokObjectBase<AuctionInfoDto, Auction, AuctionŠtokFilterDto, IQuery<Auction>>
    {
        public AuctionŠtokQueryObject(IMapper fapper, IQuery<Auction> query) : base(fapper, query)
        {
        }

        protected override IQuery<Auction> ApplyWhereClause(IQuery<Auction> query, AuctionŠtokFilterDto filter)
        {
            var definedŠtokPredicates = new List<IPredicate>();
            AddIfŠtokDefined(FilterŠtokAuctionNames(filter), definedŠtokPredicates);
            AddIfŠtokDefined(FilterŠtokAuctionPrices(filter), definedŠtokPredicates);

            if (definedŠtokPredicates.Count == 0)
            {
                return query;
            }

            if (definedŠtokPredicates.Count == 1)
            {
                return query.Where(definedŠtokPredicates.First());
            }
            
            var whereŠtokPredicate = new CompositePredicate(definedŠtokPredicates);
            return query.Where(whereŠtokPredicate);

        }

        private static void AddIfŠtokDefined(IPredicate predicate, ICollection<IPredicate> collection)
        {
            if (predicate == null)
            {
                return;
            }
            
            collection.Add(predicate);
        }

        private static IPredicate FilterŠtokAuctionPrices(AuctionŠtokFilterDto filter)
        {
            if (filter.MinimalPrice <= 0 && filter.MaximalPrice == double.MaxValue)
            {
                return null;
            }

            if (filter.MinimalPrice > 0 && filter.MaximalPrice < double.MaxValue)
            {
                return new CompositePredicate(new List<IPredicate>
                {
                    new SimplePredicate(nameof(Auction.ActualPrice), ValueComparingOperator.GreaterThanOrEqual, filter.MinimalPrice),
                    new SimplePredicate(nameof(Auction.ActualPrice), ValueComparingOperator.LessThanOrEqual, filter.MaximalPrice)
                });
            }

            if (filter.MinimalPrice > 0)
            {
                return new SimplePredicate(nameof(Auction.ActualPrice), ValueComparingOperator.GreaterThan, filter.MinimalPrice);
            }

            return new SimplePredicate(nameof(Auction.ActualPrice), ValueComparingOperator.LessThanOrEqual,
                filter.MaximalPrice);
        }

        private static SimplePredicate FilterŠtokAuctionNames(AuctionŠtokFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.AuctionName))
            {
                return null;
            }
            
            return new SimplePredicate(nameof(Auction.Name), ValueComparingOperator.StringContains, filter.AuctionName);
            
        }
        
       
    }
}