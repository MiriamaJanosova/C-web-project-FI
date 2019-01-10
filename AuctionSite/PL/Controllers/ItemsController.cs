using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.DTOs.Item;
using BL.Facades;
using Castle.Core.Internal;
using Microsoft.ApplicationInsights.Web;
using Microsoft.AspNet.Identity;
using PL.Controllers.Common;
using PL.Models.Category;

namespace PL.Controllers
{
    public class ItemsController : BaseController
    {
        public ItemFacade ItemFacade { get; set; }
        public CategoryFacade CategoryFacade { get; set; }

        public ItemsController(ItemFacade ItemFacade, CategoryFacade categoryFacade)
        {
            this.ItemFacade = ItemFacade;
            CategoryFacade = categoryFacade;
        }

        // GET: Items
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Create()
        {
            var categories = await CategoryFacade.GetAllCategoriesAsync();
            return View("Create", new CreateCategoryModel(categories.Items.ToList()));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(CreateCategoryModel model)
        {
            var dto = model.CreateItem;
            dto.OwnerID = UserId;
            var id = await ItemFacade.Create(dto);
            if (!model.SelectedCategories.IsNullOrEmpty())
            {
                var forUpdate = await ItemFacade.GetItemsById(id);
                var join = CreateCategoriesJoin(model.SelectedCategories, forUpdate.Id);
                forUpdate.HasCategories = join;
                await ItemFacade.EditItem(forUpdate);
            }
            
            return RedirectToAction("MyItems", "Account");
         }

        private List<ItemCategoryDto> CreateCategoriesJoin(IEnumerable<int> list, int itemId)
        {
            return list.Select(i => new ItemCategoryDto {CategoryID = i, ItemID = itemId}).ToList();
        }

    }
}