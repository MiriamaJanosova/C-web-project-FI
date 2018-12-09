using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BL.DTOs.Base;
using BL.Facades;
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
            return View("AddReview",new ReviewDto
            {
                ReviewedUserID = userId
            });
        }

        [HttpPost]
        public async Task<ActionResult> AddReview(ReviewDto model)
        {
            if (!ModelState.IsValid) return View(model);
            await (ReviewFacade.AddUserReviewAsync(model));
            return RedirectToAction("Index", "Users");
        }
    }
}