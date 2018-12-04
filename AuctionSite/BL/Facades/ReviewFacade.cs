using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.Facades.Base;
using BL.Services.Common;
using BL.Services.Reviews;
using BL.Services.Users;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class ReviewFacade : FacadeBase
    {
        private readonly IUserService userService;
        private readonly IReviewService reviewService;


        public ReviewFacade(IUnitOfWorkProvider unitOfWorkProvider,
            IReviewService reviewService,
            IUserService userService)
            : base(unitOfWorkProvider)
        {
            this.userService = userService;
            this.reviewService = reviewService;
        }


        public async Task<int> AddUserReviewAsync(ReviewDto review)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await userService.GetAsync(review.ReviewedUserID) == null)
                {
                    throw new ArgumentException("User does not exist.");
                }

                var res = reviewService.Create(review);
                await uow.Commit();
                return res;
            }
        }

        public async Task<ReviewDto> GetReviewByIdAsync(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await reviewService.GetAsync(id));
            }
        }


        public async Task<IEnumerable<ReviewDto>> GetReviewsForUserAsync(UserDto user)
        {
            using (UnitOfWorkProvider.Create())
            {

                var reviews = await reviewService.GetReviewForUserAsync(user.Id);
                return reviews.Items.ToList();
            }
        }

        public async Task<double> GetUserReviewAverage(UserDto user)
        {
            using (UnitOfWorkProvider.Create())
            {
                var result = await reviewService.GetReviewForUserAsync(user.Id);
                double sum = result.Items.Sum(r => Convert.ToDouble(r.Evaluation));
                return Math.Round(sum / result.Items.Count(), 1);
            }
        }

        public async Task<IDictionary<int, List<ReviewDto>>> GetUsersWithWantedEvaluation(int[] evaluations)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await reviewService.GetReviewsWithEvaluationAsync(evaluations);
            }
        }

        public async Task<IEnumerable<KeyValuePair<UserDto, double>>> Get10UsersWithTheBestEvaluation()
        {
            using (UnitOfWorkProvider.Create())
            {
                var users = (await userService.ListAllAsync()).Items;
                var dict = new Dictionary<UserDto, double>();
                foreach (var user in users)
                {
                    var reviews = (await reviewService.GetReviewForUserAsync(user.Id)).Items;
                    var reviewDtos = reviews as IList<ReviewDto> ?? reviews.ToList();
                    var reviewCount = reviewDtos.Count();
                    var totalScore = reviewDtos.Sum(rev => (double)rev.Evaluation);
                    dict.Add(user, totalScore / reviewCount);
                }
                return dict.OrderByDescending(pair => pair.Value).Take(10);
            }
        }

        public async Task<IOrderedEnumerable<KeyValuePair<int, double>>> OrderUsersByReviewEvaluation()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await reviewService.GetUsersWithAvgRatings()).OrderByDescending(dict => dict.Value);
            }
        }

        public async Task<bool> EditUserReview(ReviewDto review)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await reviewService.GetAsync(review.Id, false) == null)
                {
                    return false;
                }
                await reviewService.Update(review);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteUserReview(ReviewDto review)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var rev = await reviewService.GetAsync(review.Id, false);
                if (rev == null) return false;
                reviewService.Delete(rev.Id);
                await uow.Commit();
                return true;
            }
        }
    }
}
