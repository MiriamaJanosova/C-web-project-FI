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
using Castle.Core.Internal;
using Microsoft.AspNet.Identity;
using PL.Controllers.Common;
using Microsoft.Owin.Security;
using PL.Models.Auctions;

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
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginUser dto, string returnUrl)
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

            if (returnUrl.IsNullOrEmpty())
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(returnUrl);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(CreateUser dto)
        {
            if (!ModelState.IsValid) return View();
            var result = await UserFacade.CreateAsync(dto);
            if (result.Succeeded)
            {
                var model = new LoginUser
                {
                    UserName = dto.UserName,
                    Password = dto.Password
                };

                return Login(model, "");
            }
            else
            {
                TempData["Error"] = result.ToString();
            }

            return View();
        }

        [HttpGet]
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


        [HttpGet]
        public async Task<ActionResult> MyAuctions()
        {
            var userDTO = await UserFacade.GetUserByIdAsync(User.Identity.GetUserId<int>());
            return View(new MyAuctionsModel(userDTO.AuctionsCreated));
        }

        [HttpGet]
        public async Task<ActionResult> MyItems()
        {
            var userDTO = await UserFacade.GetUserByIdAsync(User.Identity.GetUserId<int>());
            return View(userDTO.Inventory);
        }
    }
}