using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Reviews
{
    public class ReviewService :
         CrudQueryServiceBase<Review, ReviewDto, ReviewFilterDto>,
         IReviewService
    {
        private readonly QueryObjectBase<UserDto, User, UserFilterDto, IQuery<User>> userQueryObject;

        public ReviewService(IMapper mapper, IRepository<Review> repository,
            QueryObjectBase<ReviewDto, Review, ReviewFilterDto, IQuery<Review>> query,
            QueryObjectBase<UserDto, User, UserFilterDto, IQuery<User>> userQueryObject)
            : base(mapper, repository, query)
        {
            this.userQueryObject = userQueryObject;
        }

        public async Task<QueryResultDto<ReviewDto, ReviewFilterDto>> GetReviewForUserAsync(int userID)
        {
            return await Query.ExecuteQuery(new ReviewFilterDto { UserID = userID });
        }

        public async Task<IDictionary<int, List<ReviewDto>>> GetReviewsWithEvaluationAsync(int[] evaluation)
        {
            var queryResult = await Query.ExecuteQuery(new ReviewFilterDto { Evaluation = evaluation });
            return queryResult.Items
                .GroupBy(c => (int)Math.Round((double) c.Evaluation))
                .ToDictionary(c => c.Key,
                    g => g.ToList());
        }

        public async Task<IDictionary<int, double>> GetUsersWithAvgRatings()
        {
            return (await ListAllAsync()).Items
                .Join(userQueryObject.ExecuteQuery(new UserFilterDto()).Result.Items,
                    userReview => userReview.ReviewedUserID,
                    user => user.Id,
                    (review, user) => new { User = user, Evaluation = review.Evaluation })
                .GroupBy(join => join.User.Id)
                .ToDictionary(group => group.Key,
                    group => Math.Round(((double)group.Select(review => review.Evaluation).Sum() /
                                         group.Select(review => review.Evaluation).Count()), 1));
        }

        protected override async Task<Review> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }
    }
}
