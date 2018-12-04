using System;
using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;
using System.Collections.Generic;
using System.Linq;

namespace BL.QueryObjects
{
    public class AuctionQueryObject : QueryObjectBase<AuctionDto, Auction, AuctionFilterDto, IQuery<Auction>>
    {
        public AuctionQueryObject(IMapper mapper, IQuery<Auction> query) : base(mapper, query)
        {
        }

        protected override IQuery<Auction> ApplyWhereClause(IQuery<Auction> query, AuctionFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterAuctionNames(filter), definedPredicates);
            AddIfDefined(FilterAuctionPrices(filter), definedPredicates);
            AddIfDefined(FilterAuctionDate(filter), definedPredicates);
            AddIfDefined(FilterAuctioner(filter), definedPredicates);
            if (definedPredicates.Count == 0)
            {
                return query;
            }

            if (definedPredicates.Count == 1)
            {
                return query.Where(definedPredicates.First());
            }

            var whereŠtokPredicate = new CompositePredicate(definedPredicates);
            return query.Where(whereŠtokPredicate);

        }

        private static void AddIfDefined(IPredicate predicate, ICollection<IPredicate> collection)
        {
            if (predicate == null)
            {
                return;
            }

            collection.Add(predicate);
        }

        private static IPredicate FilterAuctionDate(AuctionFilterDto filter)
        {
            if (filter.ActualDateTime == DateTime.MaxValue)
            {
                return null;
            }

            return new CompositePredicate(new List<IPredicate>
            {
                new SimplePredicate(nameof(Auction.StartDate), ValueComparingOperator.LessThanOrEqual, filter.ActualDateTime),
                new SimplePredicate(nameof(Auction.StartDate), ValueComparingOperator.LessThanOrEqual, filter.ActualDateTime)
            });

        }

        private static IPredicate FilterAuctioner(AuctionFilterDto filter)
        {
            return filter.AuctionerID == 0 ? null
                : new SimplePredicate(nameof(Auction.AuctionerID), ValueComparingOperator.Equal, filter.AuctionerID);
        }

        private static IPredicate FilterAuctionPrices(AuctionFilterDto filter)
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

        private static SimplePredicate FilterAuctionNames(AuctionFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.AuctionSearchedName))
            {
                return null;
            }

            return new SimplePredicate(nameof(Auction.Name), ValueComparingOperator.StringContains, filter.AuctionSearchedName);

        }


    }
}