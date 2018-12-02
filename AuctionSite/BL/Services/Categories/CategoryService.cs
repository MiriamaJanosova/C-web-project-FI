using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using BL.Services;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BL.Services.Categories
{
    public class CategoryService :
        CrudQueryServiceBase<Category, CategoryDto, CategoryFilterDto>,
        ICategoryService
    {
        public CategoryService(IMapper mapper, IRepository<Category> categoryRepository,
            QueryObjectBase<CategoryDto, Category, CategoryFilterDto, IQuery<Category>> categoryListQuery)
            : base(mapper, categoryRepository, categoryListQuery) { }

        protected override async Task<Category> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<int[]> GetCategoryIdsByNamesAsync(params string[] names)
        {
            var enumNames = names.Select(x => (DTOs.Enums.ItemCategoryType)Enum.
                Parse(typeof(DTOs.Enums.ItemCategoryType), x)).ToList();
            var queryResult = await Query.ExecuteQuery(new CategoryFilterDto { Names = enumNames });
            return queryResult.Items.Select(category => category.ID).ToArray();
        }

    }
}
