using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BL.DTOs.Item;
using BL.Facades;
using Microsoft.AspNet.Identity;

namespace PL.Controllers
{
    public class ItemsController : Controller
    {
        public ItemFacade ItemFacade { get; set; }

        public ItemsController(ItemFacade ItemFacade)
        {
            this.ItemFacade = ItemFacade;
        }

        // GET: Items
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(CreateItem dto)
        {
            dto.OwnerID = User.Identity.GetUserId<int>();
            await ItemFacade.Create(dto);
            return RedirectToAction("MyItems", "Account");
        }
    }
}