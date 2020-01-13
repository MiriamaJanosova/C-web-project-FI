using System.Collections;
using System.Collections.Generic;
using BL.DTOs.Base;

namespace PL.Models.Category
{
    public class CategoryListModel
    {
        public IEnumerable<CategoryDto> Categories { get; set; }

        public CategoryListModel(IEnumerable<CategoryDto> categories)
        {
            Categories = categories;
        }
    }
}