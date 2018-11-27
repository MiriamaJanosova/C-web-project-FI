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
    public class ItemCategoryQueryObject : QueryObjectBase<ItemCategoryDto, ItemCategory, ItemCategoryFilterDto, IQuery<ItemCategory>>
    {
        public ItemCategoryQueryObject(IMapper mapper, IQuery<ItemCategory> query) : base(mapper, query) { }

        protected override IQuery<ItemCategory> ApplyWhereClause(IQuery<ItemCategory> query, ItemCategoryFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterUser(filter), definedPredicates);
            AddIfDefined(FilterCategory(filter), definedPredicates);
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

        private static SimplePredicate FilterUser(ItemCategoryFilterDto filter)
        {
            if (filter.ItemID == 0)
            {
                return null;
            }

            return new SimplePredicate(nameof(ItemCategory.ItemID), ValueComparingOperator.Equal,
                filter.ItemID);
        }

        private static SimplePredicate FilterCategory(ItemCategoryFilterDto filter)
        {
            if (filter.CategoryID == 0)
            {
                return null;
            }

            return new SimplePredicate(nameof(ItemCategory.CategoryID), ValueComparingOperator.Equal,
                filter.CategoryID);
        }
    }
}
