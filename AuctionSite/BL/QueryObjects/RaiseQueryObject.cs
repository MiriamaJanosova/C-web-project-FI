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
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;

namespace BL.QueryObjects
{
    public class RaiseQueryObject : QueryObjectBase<RaiseDto, Raise, RaiseFilterDto, IQuery<Raise>>
    {
        public RaiseQueryObject(IMapper mapper, IQuery<Raise> query) : base(mapper, query) { }

        protected override IQuery<Raise> ApplyWhereClause(IQuery<Raise> query, RaiseFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterUsers(filter), definedPredicates);
            AddIfDefined(FilterAmount(filter), definedPredicates);
            AddIfDefined(FilterAuction(filter), definedPredicates);
            if (definedPredicates.Count == 0)
            {
                return query;
            }
            if (definedPredicates.Count == 1)
            {
                return query.Where(definedPredicates.First());
            }
            var wherePredicate = new CompositePredicate(definedPredicates);
            return query.Where(wherePredicate);
        }

        private static void AddIfDefined(IPredicate reviewPredicate, ICollection<IPredicate> definedPredicates)
        {
            if (reviewPredicate != null)
            {
                definedPredicates.Add(reviewPredicate);
            }
        }

        private static SimplePredicate FilterUsers(RaiseFilterDto filter)
        {
            if (filter.AuctionerID <= 0)
            {
                return null;
            }
            return new SimplePredicate(nameof(Raise.UserWhoRaisedID), ValueComparingOperator.Equal, filter.AuctionerID);
        }

        private static SimplePredicate FilterAuction(RaiseFilterDto filter)
        {
            if (filter.RaiseForAuctionID <= 0)
            {
                return null;
            }
            return new SimplePredicate(nameof(Raise.RaiseForAuctionID), ValueComparingOperator.Equal, filter.RaiseForAuctionID);
        }

        private static SimplePredicate FilterAmount(RaiseFilterDto filter)
        {
            if (filter.Amount <= 0)
            {
                return null;
            }
            return new SimplePredicate(nameof(Raise.Amount), ValueComparingOperator.GreaterThanOrEqual, filter.Amount);
        }

    }
}
