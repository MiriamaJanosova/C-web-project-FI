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
    public class ReviewQueryObject : QueryObjectBase<ReviewDto, Review, ReviewFilterDto, IQuery<Review>>
    {
        public ReviewQueryObject(IMapper mapper, IQuery<Review> query) : base(mapper, query) { }

        protected override IQuery<Review> ApplyWhereClause(IQuery<Review> query, ReviewFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterUsers(filter), definedPredicates);
            AddIfDefined(FilterEvaluations(filter), definedPredicates);
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

        private static SimplePredicate FilterUsers(ReviewFilterDto filter)
        {
            if (filter.UserID <= 0)
            {
                return null;
            }
            return new SimplePredicate(nameof(User.Id), ValueComparingOperator.Equal, filter.UserID);
        }

        private static CompositePredicate FilterEvaluations(ReviewFilterDto filter)
        {
            if (filter.Evaluation != null && filter.Evaluation.Any())
            {
                return null;
            }

            var reviewsEvalPredicates = new List<IPredicate>(filter.Evaluation
                .Select(eval => new SimplePredicate
                (nameof(Review.Evaluation),
                ValueComparingOperator.Equal,
                eval)));

            return new CompositePredicate(reviewsEvalPredicates, LogicalOperator.OR);
        }
    }
}