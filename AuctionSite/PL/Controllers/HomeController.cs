using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PL.Controllers.Common;
using PL.Models.Home;

namespace PL.Controllers
{
    public class HomeController : BaseController
    {

        public HomeController()
        {
           
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new IndexModel());
        }

    }
}