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
    public class CategoryQueryObject : QueryObjectBase<CategoryDto, Category, CategoryFilterDto, IQuery<Category>>
    {
        public CategoryQueryObject(IMapper mapper, IQuery<Category> query) : base(mapper, query) { }

        protected override IQuery<Category> ApplyWhereClause(IQuery<Category> query, CategoryFilterDto filter)
        {
            if (filter.Names == null || !filter.Names.Any())
            {
                return query;
            }
            var categoryNamePredicates = new List<IPredicate>(filter.Names
                .Select(name => new SimplePredicate(
                    nameof(Category.CategoryType),
                    ValueComparingOperator.Equal,
                    name)));
            var predicate = new CompositePredicate(categoryNamePredicates, LogicalOperator.OR);
            return query.Where(predicate);
        }
    }
}
