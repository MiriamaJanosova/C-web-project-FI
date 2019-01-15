using System;
using System.Collections.Generic;
using System.IO;
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
        public UserFacade UserFacade { get; set; }

        public ModifyAuctionsFacade ModifyAuctionFacade { get; set; }

        public ItemFacade ItemFacade { get; set; }


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

            foreach (var error in result.Errors)
            {
                TempData["Error"] += error;
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
        public async Task<ActionResult> MyAuctions(int pageOn = 1, int pageEn = 1)
        {
            var userDTO = await UserFacade.GetUserByIdAsync(User.Identity.GetUserId<int>());
            var model = new MyAuctionsModel(userDTO.AuctionsCreated, pageEn, pageOn)
            {
                Ended = {IsPrivate = true}, Ongoing = {IsPrivate = true}
            };
            return View("MyAuctions", model);
        }

        [HttpGet]
        public async Task<ActionResult> MyItems()
        {
            var userDTO = await UserFacade.GetUserByIdAsync(User.Identity.GetUserId<int>());
            return View(userDTO.Inventory);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Create()
        {
            var avail = await ModifyAuctionFacade.GetAvailableItemsOfUser(UserId);
            return View(new CreateAuctionModel
            {
                AvailableItems = avail.ToList()
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(CreateAuctionModel model)
        {
            if (!ModelState.IsValid)
                return View();
            

            var dto = model.Dto;

            foreach (var file in dto.Upload)
            {
                if (file?.InputStream == null)
                {
                    continue;
                }   
                dto.ImageBytes.Add(new ImageDto(await ImageToByteArray(file.InputStream)));
            }
            
            dto.UserId = User.Identity.GetUserId<int>();
            dto.StartPrice = CurrencyController.CalcCurrencyAndGetSymbol(dto.StartPrice, true).Item1;
            dto.ActualPrice = dto.StartPrice;
            var res = await ModifyAuctionFacade.AddAuctionAsync(dto);
            if (res == 0) 
            {
                TempData["ErrorMessage"] = "Adding item failed";
                return View();
            }

            await AssignAuctionToItems(res, model.SelectedItems);
            return RedirectToAction("MyAuctions", "Account");
        }
        
        private static async Task<byte[]> ImageToByteArray(Stream input)
        {
            var ms = new MemoryStream();
            await input.CopyToAsync(ms);
            return ms.ToArray();
        }
        
        private async Task AssignAuctionToItems(int auctionId, IList<int> itemIds)
        {
            foreach (var id in itemIds)
            {
                var item = await ModifyAuctionFacade.GetItem(id);
                item.AuctionID = auctionId;
                await ItemFacade.AssignItemToAuction(item);
            }
        }
    }
}