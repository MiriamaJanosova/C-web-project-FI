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
            AddIfDefined(FilterEvaluation(filter), definedPredicates);
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

        private static CompositePredicate FilterEvaluation(UserFilterDto filter)
        {
            if (filter.UserEvaluation == null || filter.UserEvaluation.Any())
            {
                return null;
            }

            var reviewsEvalPredicates = new List<IPredicate>(filter.UserEvaluation
                .Select(eval => new SimplePredicate
                (nameof(Review.Evaluation),
                ValueComparingOperator.Equal,
                eval)));

            return  new CompositePredicate(reviewsEvalPredicates, LogicalOperator.OR);            
        }
    }
}
