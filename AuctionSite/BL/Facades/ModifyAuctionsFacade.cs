using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.Facades.Base;
using BL.QueryObjects.Common;
using BL.Services.Auctions;
using BL.Services.Categories;
using BL.Services.Items;
using BL.Services.Raises;
using BL.Services.Reviews;
using BL.Services.Users;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class ModifyAuctionsFacade : FacadeBase
    {
        private readonly IAuctionService auctionService;
        private readonly IItemService itemService;
        private readonly ICategoryService categoryService;
        private readonly IItemCategoryService itemCategoryService;
        private readonly IUserService userService;
        private readonly IRaiseService raiseService;

        public ModifyAuctionsFacade(
            IUnitOfWorkProvider provider,
            IAuctionService auctionService,
            IItemService itemService,
            ICategoryService categoryService,
            IItemCategoryService itemCategoryService,
            IUserService userService,
            IRaiseService raiseService)
            : base(provider)
        {
            this.auctionService = auctionService;
            this.itemService = itemService;
            this.categoryService = categoryService;
            this.itemCategoryService = itemCategoryService;
            this.userService = userService;
            this.raiseService = raiseService;
        }

        public async Task<int> AddAuctionAsync(AuctionDto auction)
        {
            if (auction == null)
            {
                return 0;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await userService.GetAsync(auction.AuctionerID, false) == null)
                {
                    return 0;
                }

                var res = auctionService.Create(auction);
                await uow.Commit();
                return res;
            }
        }

        public async Task<int> AddItems(ItemDto item)
        {
            if (item == null)
            {
                return 0;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                var auction = await auctionService.GetAsync(item.AuctionID);
                if (auction == null)
                {
                    return 0;
                }

                var res = itemService.Create(item);
                await uow.Commit();
                return res;
            }
        }

        public async Task<int> AddCategory(CategoryDto category)
        {
            if (category == null)
            {
                return 0;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                var res = categoryService.Create(category);
                await uow.Commit();
                return res;
            }
        }

        public async Task<int> AddItemCategoryAsync(ItemCategoryDto itemCategory)
        {
            if (itemCategory == null)
            {
                return 0;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                var item = await itemService.GetAsync(itemCategory.ItemID);
                if (item == null)
                {
                    return 0;
                }

                var category = await categoryService.GetAsync(itemCategory.CategoryID);
                if (category == null)
                {
                    return 0;
                }

                var res = itemCategoryService.Create(itemCategory);
                await uow.Commit();
                return res;
            }
        }

        public async Task<int> AddRaiseAsync(RaiseDto raise)
        {
            if (raise == null)
            {
                return 0;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await auctionService.GetAsync(raise.RaiseForAuctionID, false) == null)
                {
                    return 0;
                }

                if (await userService.GetAsync(raise.UserWhoRaisedID, false) == null)
                {
                    return 0;
                }

                var res = raiseService.Create(raise);
                await uow.Commit();
                return res;
            }
        }

        public async Task<bool> DeleteAuctionAsync(int auctionID)
        {
            if (auctionID == 0)
            {
                return false;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await auctionService.GetAsync(auctionID) == null)
                {
                    return false;
                }

                var items = (await itemService.GetItemsByAuctionIDAsync(auctionID));
                foreach (var item in items)
                {
                    var itemCategories = (await itemCategoryService.GetCategoryByItemIdsAsync(item.Id));
                    foreach (var itemCategory in itemCategories)
                    {
                        itemCategoryService.Delete(itemCategory.Id);
                    }

                    itemService.Delete(item.Id);
                }

                var raises = (await raiseService.GetRaisesByAuctionIDAsync(auctionID)).Items;
                foreach (var raise in raises)
                {
                    raiseService.Delete(raise.Id);
                }

                auctionService.Delete(auctionID);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int categoryID)
        {
            if (categoryID == 0)
            {
                return false;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await categoryService.GetAsync(categoryID) == null)
                {
                    return false;
                }

                var itemCategories = (await itemCategoryService.GetItemCategoriesByCategoryIdAsync(categoryID));
                foreach (var itemCategory in itemCategories)
                {
                    itemCategoryService.Delete(itemCategory.Id);
                }

                categoryService.Delete(categoryID);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteRaiseAsync(int raiseID)
        {
            if (raiseID == 0)
            {
                return false;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await raiseService.GetAsync(raiseID) == null)
                {
                    return false;
                }

                raiseService.Delete(raiseID);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteItemAsync(int itemID)
        {
            if (itemID == 0)
            {
                return false;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await itemService.GetAsync(itemID) == null)
                {
                    return false;
                }

                var itemCategories = (await itemCategoryService.GetCategoryByItemIdsAsync(itemID));
                foreach (var itemCategory in itemCategories)
                {
                    itemCategoryService.Delete(itemCategory.Id);
                }

                itemService.Delete(itemID);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteItemCategory(int itemCategoryId)
        {
            if (itemCategoryId == 0)
            {
                return false;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await itemCategoryService.GetAsync(itemCategoryId) == null)
                {
                    return false;
                }

                itemCategoryService.Delete(itemCategoryId);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> UpdateItemsAsync(ItemDto item)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await itemService.GetAsync(item.Id) == null)
                {
                    return false;
                }

                await itemService.Update(item);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> UpdateAuctionAsync(AuctionDto auction)
        {
            if (auction == null)
            {
                return false;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await auctionService.GetAsync(auction.Id) == null)
                {
                    return false;
                }

                await auctionService.Update(auction);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> UpdateCategoryAsync(CategoryDto category)
        {
            if (category == null)
            {
                return false;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await categoryService.GetAsync(category.Id) == null)
                {
                    return false;
                }

                await categoryService.Update(category);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> UpdateRaiseAsync(RaiseDto raise)
        {
            if (raise == null)
            {
                return false;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await raiseService.GetAsync(raise.Id) == null)
                {
                    return false;
                }

                await raiseService.Update(raise);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> UpdateItemCategoryAsync(ItemCategoryDto itemCategory)
        {
            if (itemCategory == null)
            {
                return false;
            }
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await itemCategoryService.GetAsync(itemCategory.Id) == null)
                {
                    return false;
                }

                await itemCategoryService.Update(itemCategory);
                await uow.Commit();
                return true;
            }
        }

        public async Task<QueryResultDto<AuctionDto, AuctionFilterDto>> GetAllAuctionsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await auctionService.ListAllAsync();
            }
        }

        public async Task<QueryResultDto<ItemDto, ItemFilterDto>> GetAllItemsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await itemService.ListAllAsync();
            }
        }

        public async Task<QueryResultDto<CategoryDto, CategoryFilterDto>> GetAllCategoriesAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await categoryService.ListAllAsync();
            }
        }

        public async Task<QueryResultDto<ItemCategoryDto, ItemCategoryFilterDto>> GetAllItemCategoriesAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await itemCategoryService.ListAllAsync();
            }
        }

        public async Task<QueryResultDto<RaiseDto, RaiseFilterDto>> GetAllRaisesAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await raiseService.ListAllAsync();
            }
        }
    }
}
