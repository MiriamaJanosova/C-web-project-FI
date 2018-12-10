using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.DTOs.Item;
using BL.Facades.Base;
using BL.QueryObjects.Common;
using BL.Services.Auctions;
using BL.Services.Categories;
using BL.Services.Items;
using BL.Services.Users;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class ItemFacade : FacadeBase
    {
        private readonly ICategoryService categoryService;
        private readonly IItemCategoryService itemCategoryService;
        private readonly IItemService itemService;
        private readonly IMapper mapper;

        public ItemFacade(IUnitOfWorkProvider provider, ICategoryService categoryService,
            IItemService itemService, IItemCategoryService itemCategoryService, IMapper mapper)
            : base(provider)
        {
            this.categoryService = categoryService;
            this.itemService = itemService;
            this.itemCategoryService = itemCategoryService;
            this.mapper = mapper;
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await categoryService.GetAsync(id);
            }
        }

        public async Task<ItemDto> GetItemsById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await itemService.GetAsync(id);
            }
        }



        public async Task<QueryResultDto<ItemDto, ItemFilterDto>> GetAllItemsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await itemService.ListAllAsync();
            }
        }

        public async Task<IEnumerable<ItemDto>> GetItemForCategories(List<ItemCategoryDto> itemCategories)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await itemService.GetItemsByCategoriesAsync(itemCategories);
            }
        }

        public async Task<IEnumerable<ItemDto>> GetFilteredItems(ItemFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                if (filter == null)
                {
                    var temp = await itemService.ListAllAsync();
                    return temp.Items;
                }

                return await itemService.ListFilteredItems(filter);
            }
        }



        public async Task<bool> ContainsItem(ItemDto item)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await itemService.GetAsync(item.Id) != null;
            }
        }

        public async Task<int> Create(CreateItem dto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var item = itemService.Create(itemService.MapToBase(dto));
                await uow.Commit();
                return item.Id;
            }
        }

    }
}
