using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BL.DTOs.Base;
using BL.Facades;

namespace WebAPI.Controllers
{
    public class ReviewsController : ApiController
    {
        public ReviewFacade ReviewsFacade { get; set; }
        public UserFacade UserFacade { get; set; }

        public async Task<IEnumerable<ReviewDto>> GetReviewsForUser(UserDto user)
        {
            if (await UserFacade.GetUserAccordingToEmailAsync(user.Email) == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var reviews = await ReviewsFacade.GetReviewsForUserAsync(user);
            foreach (var review in reviews)
            {
                review.Id = 0;
            }

            return reviews;
        }

        public async Task<double> GetReviewAverageForUser(UserDto user)
        {
            if (await UserFacade.GetUserAccordingToEmailAsync(user.Email) == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return await ReviewsFacade.GetUserReviewAverage(user);
        }

        public async Task<string> Post([FromBody]ReviewDto review)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            if (await UserFacade.GetUserAccordingToEmailAsync(review.ReviewedUser.Email) == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var res = await ReviewsFacade.AddUserReviewAsync(review);
            if (res == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return $"Created review for user {review.ReviewedUser.UserName} with id: {res}.";
        }

        
        public async Task<string> Put([FromBody] ReviewDto review)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var success = await ReviewsFacade.EditUserReview(review);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Updated review for user: {review.ReviewedUser.UserName}";
        }

        public async Task<string> DeleteReview(int id)
        {
            var review = await ReviewsFacade.GetReviewByIdAsync(id);
            if (review == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var success = await ReviewsFacade.DeleteUserReview(review);
            if (!success)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return $"Deleted review with id: {id}";
        }
    }
}
