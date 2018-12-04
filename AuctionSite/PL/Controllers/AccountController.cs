using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BL.DTOs.Base;
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
            get { return HttpContext.GetOwinContext().Authentication; }
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
            if (!ModelState.IsValid) return View();
            try
            {
                var result = UserFacade.Login(dto.UserName, dto.Password);
                AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = false}, result);
            }
            catch (Exception)
            {
                TempData["Error"] = "Couldn't login";
                return View();
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<ActionResult> Info()
        {
            var dto = await UserFacade.GetUserByIdAsync(int.Parse(User.Identity.GetUserId()));
            var userModel = UserFacade.ConvertUserDtoToSettingPage(dto);
            return View("UserInfo", userModel);
        }
        
        [HttpPost]
        public async Task<ActionResult> Info(UserShowSettingPage dto)
        {
            await UserFacade.UpdateUserInfo(dto);
            TempData["Success"] = "operation successful";
            return View("UserInfo", dto);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(CreateUser dto)
        {
            if (!ModelState.IsValid) return View();
            var result = await UserFacade.CreateAsync(dto);
            if (result.Succeeded)
            {
                TempData["Info"] = result.ToString();
            }
            else
            {
                TempData["Error"] = result.ToString();
            }
            return View();
        }
    }
}