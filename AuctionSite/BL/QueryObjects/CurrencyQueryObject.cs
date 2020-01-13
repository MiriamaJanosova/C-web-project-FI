using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using Castle.Core.Internal;
using DAL.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;

namespace BL.QueryObjects
{
    public class CurrencyQueryObject : QueryObjectBase<CurrencyDto, Currency, CurrencyFilterDto, IQuery<Currency>>
    {
        public CurrencyQueryObject(IMapper mapper, IQuery<Currency> query) : base(mapper, query)
        {
        }

        protected override IQuery<Currency> ApplyWhereClause(IQuery<Currency> query, CurrencyFilterDto filter)
        {
            if (filter.CodeName.IsNullOrEmpty())
            {
                return query;
            }

            var predicate = new SimplePredicate(nameof(Currency.Code), ValueComparingOperator.Equal, filter.CodeName);
            return query.Where(predicate);
        }
    }
}
    