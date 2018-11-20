using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BL.Facades;
using PL.Controllers.Common;

namespace PL.Controllers
{
    public class ReviewController : BaseController
    {

        public UserFacade UserFacade { get; set; }

        // GET: Review
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> User(int id)
        {
            var user = await UserFacade.GetUserById(id);
            if (user == null)
                return Error();
            TempData["UserEmail"] = user.Email;
            return View(user.Reviews);
        }
    }
}