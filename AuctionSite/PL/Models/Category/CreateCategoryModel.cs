<<<<<<< HEAD
﻿using System;
=======
using System;
>>>>>>> origin/marek-branch
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
<<<<<<< HEAD
=======

        public CreateCategoryModel()
        {
            AvailableCategories = new List<CategoryDto>();
            SelectedCategories = new List<int>();
        }
>>>>>>> origin/marek-branch
    }
}