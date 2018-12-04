using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entities;

namespace BL.QueryObjects
{
    public class ItemQueryObject : QueryObjectBase<ItemDto, Item, ItemFilterDto, IQuery<Item>>
    {
        public ItemQueryObject(IMapper mapper, IQuery<Item> query) : base(mapper, query) { }

        protected override IQuery<Item> ApplyWhereClause(IQuery<Item> query, ItemFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterName(filter), definedPredicates);
            AddIfDefined(FilterCategories(filter), definedPredicates);
            AddIfDefined(FilterUser(filter), definedPredicates);
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

        private static void AddIfDefined(IPredicate categoryPredicate, ICollection<IPredicate> definedPredicates)
        {
            if (categoryPredicate != null)
            {
                definedPredicates.Add(categoryPredicate);
            }
        }

        private static SimplePredicate FilterName(ItemFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.SearchedName))
            {
                return null;
            }

            return new SimplePredicate(nameof(Item.Name), ValueComparingOperator.StringContains,
                filter.SearchedName);
        }

        private static SimplePredicate FilterUser(ItemFilterDto filter)
        {
            if (filter.OwnerID == 0)
            {
                return null;
            }

            return new SimplePredicate(nameof(Item.OwnerID), ValueComparingOperator.Equal,
                filter.OwnerID);
        }

        private static SimplePredicate FilterAuction(ItemFilterDto filter)
        {
            if (filter.AuctionID == 0)
            {
                return null;
            }

            return new SimplePredicate(nameof(Item.AuctionID), ValueComparingOperator.Equal,
                filter.AuctionID);
        }

        private static CompositePredicate FilterCategories(ItemFilterDto filter)
        {
            if (filter.ItemCategoryTypes == null || filter.ItemCategoryTypes.Any())
            {
                return null;
            }

            var categoryTypesPredicates = new List<IPredicate>(filter.ItemCategoryTypes
                .Select(cat => new SimplePredicate
                (nameof(ItemCategory.Id),
                ValueComparingOperator.Equal,
                cat.ID)));

            return new CompositePredicate(categoryTypesPredicates, LogicalOperator.OR);
        }
    }
}