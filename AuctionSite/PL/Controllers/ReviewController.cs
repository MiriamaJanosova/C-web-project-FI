using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.WebPages;
using BL.DTOs.Base;
using BL.Facades;
using Microsoft.AspNet.Identity;
using PL.Controllers.Common;

namespace PL.Controllers
{
    public class ReviewController : BaseController
    {

        public UserFacade UserFacade { get; set; }

        public ReviewFacade ReviewFacade { get; set; }

        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> User(int id)
        {
            var user = await UserFacade.GetUserByIdAsync(id);
            if (user == null)
                return Error();
            TempData["UserEmail"] = user.Email;
            return View(user.Reviews);
        }

        public async Task<ActionResult> AddReview(int userId)
        {
            if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Denied", "Base");

            }
            return View("Detail",new ReviewDto
            {
                ReviewedUserID = userId,
                UserWhoWroteID = System.Web.HttpContext.Current.User.Identity.GetUserId().AsInt()
            });
        }

        [HttpPost]
        public async Task<ActionResult> AddReview(ReviewDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await (ReviewFacade.AddUserReviewAsync(model));
            return RedirectToAction("Index", "Users");
        }

        [HttpGet]
        public async Task<ActionResult> UpdateReview(string description, int evaluation, int userId, int userWhoRev)
        {
            if (System.Web.HttpContext.Current.User.Identity.GetUserId().AsInt() != userWhoRev)
            {
                return RedirectToAction("Denied", "Base");
            }

            return View("Detail", new ReviewDto
            {
                Description = description,
                Evaluation = evaluation,
                ReviewedUserID = userId,
                UserWhoWroteID = userWhoRev
            });
        }

        [HttpPost]
        public async Task<ActionResult> UpdateReview(ReviewDto review)
        {
            if (System.Web.HttpContext.Current.User.Identity.GetUserId().AsInt() != review.UserWhoWroteID)
            {
                return RedirectToAction("Denied", "Base");
            }

            if (await ReviewFacade.EditUserReview(review))
            {
                return RedirectToAction("Index", "Users");
            }

            return RedirectToAction("Detail", "Review", new {review.Description, review.Evaluation,
                review.ReviewedUserID, review.UserWhoWroteID});
        }

        [HttpGet]
        public async Task<ActionResult> DeleteReview(ReviewDto review)
        {
            if (System.Web.HttpContext.Current.User.Identity.GetUserId().AsInt() != review.UserWhoWroteID)
            {
                return RedirectToAction("Denied", "Base");
            }
            await (ReviewFacade.DeleteUserReview(review));
            return RedirectToAction("Index", "Users");
        }
    }
}