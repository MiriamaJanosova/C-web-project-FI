using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.UnitOfWork;
using PL.Models.Home;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        private IActionService _aus;

        public HomeController(IActionService aus)
        {
            _aus = aus;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(IndexModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var res = await _aus.GetAutionByName(model.Search);
            return RedirectToAction($"Auction/{res.AuctionID}");

            throw new NotImplementedException();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}