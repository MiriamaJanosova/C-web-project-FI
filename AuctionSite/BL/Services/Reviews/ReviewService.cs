using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Reviews
{
    public class ReviewService :
         CrudQueryServiceBase<Review, ReviewDto, ReviewFilterDto>,
         IReviewService
    {
        public ReviewService(IMapper mapper, IRepository<Review> repository, 
            QueryObjectBase<ReviewDto, Review, ReviewFilterDto, IQuery<Review>> query) 
            : base(mapper, repository, query) {}

        public async Task<ReviewDto> GetReviewForUserAsync(int userID)
        {
            var queryResult = await Query.ExecuteQuery(new ReviewFilterDto { UserID = userID });
            return queryResult.Items.SingleOrDefault();
        }

        protected async override Task<Review> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }
    }
}
