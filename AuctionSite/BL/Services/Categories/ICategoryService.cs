using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using BL.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Categories
{
    public interface ICategoryService : IService<CategoryDto, CategoryFilterDto>
    {
        Task<int[]> GetCategoryIdsByNamesAsync(params string[] names);

        Task<CategoryDto> GetAsync(int entityId, bool withIncludes = true);

        int Create(CategoryDto entityDto);

        Task Update(CategoryDto entityDto);

        void Delete(int entityId);

    }
}

