using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;

namespace BL.Services.Categories
{
    public class ItemCategoryService :
        CrudQueryServiceBase<ItemCategory, ItemCategoryDto, ItemCategoryFilterDto>,
        IItemCategoryService
    {
        public ItemCategoryService(IMapper mapper, IRepository<ItemCategory> repository,
            QueryObjectBase<ItemCategoryDto, ItemCategory, ItemCategoryFilterDto, IQuery<ItemCategory>> query)
            : base(mapper, repository, query)
        {
        }

        protected override async Task<ItemCategory> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }

        public async Task<IEnumerable<ItemCategoryDto>> GetCategoryByItemIdsAsync(int itemId)
        {
            var queryResult = await Query.ExecuteQuery(new ItemCategoryFilterDto { ItemID = itemId });
            return queryResult.Items.ToList();
        }

        public async Task<IEnumerable<ItemCategoryDto>> GetItemCategoriesByCategoryIdAsync(int categoryId)
        {
            var queryResult = await Query.ExecuteQuery(new ItemCategoryFilterDto { CategoryID = categoryId });
            return queryResult.Items.ToList();
        }
    }
}
