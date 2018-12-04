using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using BL.Services.Common;
using DAL.Entities;

namespace BL.Services.Categories
{
    public interface IItemCategoryService : IService<ItemCategoryDto, ItemCategoryFilterDto>
    {
        Task<IEnumerable<ItemCategoryDto>> GetCategoryByItemIdsAsync(int itemId);

        Task<IEnumerable<ItemCategoryDto>> GetItemCategoriesByCategoryIdAsync(int categoryId);

        Task<ItemCategoryDto> GetAsync(int entityId, bool withIncludes = true);

        int Create(ItemCategoryDto entityDto);

        Task Update(ItemCategoryDto entityDto);

        void Delete(int entityId);

    }
}
