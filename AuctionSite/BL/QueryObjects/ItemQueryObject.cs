using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        private static CompositePredicate FilterCategories(ItemFilterDto filter)
        {
            if (filter.CategoryTypes == null || filter.CategoryTypes.Any())
            {
                return null;
            }

            var categoryTypesPredicates = new List<IPredicate>(filter.CategoryTypes
                .Select(cat => new SimplePredicate
                (nameof(Category.CategoryType),
                ValueComparingOperator.Equal,
                cat)));

            return new CompositePredicate(categoryTypesPredicates, LogicalOperator.OR);
        }
    }
}