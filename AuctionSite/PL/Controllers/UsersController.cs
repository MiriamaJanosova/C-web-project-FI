using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BL.Facades;
using PL.Controllers.Common;
using PL.Models.Users;

namespace PL.Controllers
{
    public class UsersController : BaseController
    {

        public UserFacade UserFacade { get; set; }
        
        public async Task<ActionResult> Index()
        {
            var users = await UserFacade.GetUsersAsync();
            if (users == null)
                return Error();
            return View("UserList", new UserListModel(users));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            if (UserId == id)
            {
                return await Index();
            }
            
            await UserFacade.DeleteUserAsync(id);
            return await Index();
        }
    }
}