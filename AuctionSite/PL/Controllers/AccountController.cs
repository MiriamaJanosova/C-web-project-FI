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
        public UserFacade UserFacade;

        public AccountController(UserFacade UserFacade)
        {
            this.UserFacade = UserFacade;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            // TODO
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUser dto)
        {
            if (ModelState.IsValid)
            {
                var result = UserFacade.Login(dto.Email, dto.Password);
                if (result.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }

                TempData["Error"] = "Couldn't login";
            }
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
                    var res = UserFacade.Login(dto.Email, dto.Password);
                    if (res.IsAuthenticated)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                TempData["Error"] = result.ToString();
            }

            
            return View();
        }
    }
}