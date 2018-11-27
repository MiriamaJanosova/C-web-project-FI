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
    public class UserQueryObject : QueryObjectBase<UserDto, User, UserFilterDto, IQuery<User>>
    {
        public UserQueryObject(IMapper mapper, IQuery<User> query) : base(mapper, query) { }

        protected override IQuery<User> ApplyWhereClause(IQuery<User> query, UserFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterEmail(filter), definedPredicates);
            AddIfDefined(FilterUserName(filter), definedPredicates);
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

        private static SimplePredicate FilterEmail(UserFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.UserEmail))
            {
                return null;
            }

            return new SimplePredicate(nameof(User.Email), ValueComparingOperator.Equal,
                filter.UserEmail);
        }

        private static SimplePredicate FilterUserName(UserFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.UserName))
            {
                return null;
            }

            return new SimplePredicate(nameof(User.UserName), ValueComparingOperator.Equal,
                filter.UserName);
        }

    }
}
