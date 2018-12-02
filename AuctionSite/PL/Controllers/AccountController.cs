using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BL.DTOs.Users;
using BL.Facades;
using BL.Identity;
using Microsoft.AspNet.Identity;
using PL.Controllers.Common;
using Microsoft.Owin.Security;

namespace PL.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public UserFacade UserFacade;
       

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        
        public AccountController(UserFacade UserFacade)
        {
            this.UserFacade = UserFacade;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            // TODO
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginUser dto)
        {
            if (ModelState.IsValid)
            {
                var result = UserFacade.Login(dto.Email, dto.Password);
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = false }, result);
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Home");
                }

                TempData["Error"] = "Couldn't login";
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        
        //[HttpPost]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
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