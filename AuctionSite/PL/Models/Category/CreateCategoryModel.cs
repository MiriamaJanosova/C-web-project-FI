using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BL.DTOs.Base;
using BL.DTOs.Item;

namespace PL.Models.Category
{
    public class CreateCategoryModel
    {
        public CreateItem CreateItem { get; } = new CreateItem();
        public IList<CategoryDto> AvailableCategories { get; set; }
        public IList<int> SelectedCategories { get; set; }

        public CreateCategoryModel(IList<CategoryDto> availableCategories)
        {
            AvailableCategories = availableCategories;
        }
    }
}