using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.Services.Auctions;
using System.Threading.Tasks;
using BL.Facades;

namespace PL.Controllers
{
    public class AuctionController : Controller
    {
        public AuctionFacade AuctionFacade { get; set; }


        public async Task<ActionResult> Index()
        {
            var all = await AuctionFacade.GetAllAuctions();
            return View(all);
        }
    }
}