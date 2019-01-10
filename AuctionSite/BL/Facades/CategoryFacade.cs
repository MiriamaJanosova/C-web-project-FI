using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.Facades.Base;
using BL.QueryObjects.Common;
using BL.Services.Categories;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class CategoryFacade : FacadeBase
    {
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;
        
        public CategoryFacade(IUnitOfWorkProvider provider, ICategoryService categoryService, IItemCategoryService itemCategoryService, IMapper mapper) 
            : base(provider)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }
        
        public async Task<CategoryDto> GetCategoryById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await categoryService.GetAsync(id);
            }
        }
        
        public async Task<QueryResultDto<CategoryDto, CategoryFilterDto>> GetAllCategoriesAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await categoryService.ListAllAsync();
            }
        }
        
        public async Task<bool> EditCategory(CategoryDto category)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await categoryService.GetAsync(category.Id, false) == null)
                {
                    return false;
                }
                await categoryService.Update(category);
                await uow.Commit();
                return true;
            }
        }
        
        public async Task<bool> DeleteCategory(int categoryId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var rev = await categoryService.GetAsync(categoryId, false);
                if (rev == null) return false;
                categoryService.Delete(rev.Id);
                await uow.Commit();
                return true;
            }
        }
    }
}