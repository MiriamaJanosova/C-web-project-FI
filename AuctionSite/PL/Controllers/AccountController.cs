using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BL.DTOs.Users;
using BL.Facades;
using BL.Identity;
using PL.Controllers.Common;

namespace PL.Controllers
{
    public class AccountController : BaseController
    {
        public UserFacade UserFacade { get; set; }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Login()
        {
            // TODO
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Register()
        {
            // TODO
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(CreateUser dto)
        {
            if (ModelState.IsValid)
            {
                var result = await UserFacade.CreateAsync(dto);
                if (result.Succeeded)
                {
                    new NotImplementedException();
                }

            }

            
            return View();
        }
    }
}