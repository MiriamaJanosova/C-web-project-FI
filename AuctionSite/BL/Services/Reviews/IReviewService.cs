using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using BL.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Reviews
{
    public interface IReviewService : IService<ReviewDto, ReviewFilterDto>
    {
        Task<QueryResultDto<ReviewDto, ReviewFilterDto>> GetReviewForUserAsync(int userID);

        Task<ReviewDto> GetAsync(int entityId, bool withIncludes = true);

        int Create(ReviewDto entityDto);

        Task Update(ReviewDto entityDto);

        void Delete(int entityId);

        Task<IDictionary<int, List<ReviewDto>>> GetReviewsWithEvaluationAsync(int[] evaluation);
        Task<IDictionary<int, double>> GetUsersWithAvgRatings();
    }
}
