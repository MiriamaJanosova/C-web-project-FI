using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PL.Models.Home;

namespace PL.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
           
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