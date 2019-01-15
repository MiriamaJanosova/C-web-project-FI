using System.Threading.Tasks;
using System.Web.Mvc;
using BL.DTOs.Base;
using BL.Facades;
using PL.Controllers.Common;
using PL.Models.Category;

namespace PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : BaseController
    {
        public CategoryFacade CategoryFacade { get; set; }
        
        public CategoryController(CategoryFacade categoryFacade)
        {
            CategoryFacade = categoryFacade;
        }

        public async Task<ActionResult> Index()
        {
            var categories = await CategoryFacade.GetAllCategoriesAsync();
            return View("CategoryList", new CategoryListModel(categories.Items));

        }

        [HttpGet]
        public async Task<ActionResult> EditCategory(int categoryId)
        {
            var category = await CategoryFacade.GetCategoryById(categoryId);
            if (category == null)
            {
                Error();
            }

            return View("CategoryDetail", category);
        }
        [HttpPost]
        public async Task<ActionResult> EditCategory(CategoryDto category)
        {
            if (!await CategoryFacade.EditCategory(category))
            {
                TempData["Error"] = "Can't edit category";
            }

            return RedirectToAction("Index");
        }
        
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            if (!await CategoryFacade.DeleteCategory(categoryId))
            {
                TempData["Error"] = "Can't delete category";
            }

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryDto category)
        {
            if (await CategoryFacade.Create(category) == 0)
            {
                TempData["Error"] = "Can't add category";
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult AddCategory(int categoryId)
        {
            
            return RedirectToAction("Index");
        }
        
    }
}