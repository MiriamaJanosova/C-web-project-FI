using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using BL.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BL.Services.Items
{
    public interface IItemService : IService<ItemDto, ItemFilterDto>
    {// <summary>
        /// Gets ids of the categories with the corresponding names
        /// </summary>
        /// <param name="names">names of the categories</param>
        /// <returns>ids of categories with specified name</returns>
        Task<IEnumerable<ItemDto>> GetItemsByNameAsync(string name);

        /// <summary>
        /// Gets DTO representing the entity according to Id
        /// </summary>
        /// <param name="entityId">entity Id</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<ItemDto> GetAsync(int entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        int Create(ItemDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(ItemDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(int entityId);

        Task<ItemDto> AddItemCategory(Category category, int itemId);

        Task<IEnumerable<ItemDto>> GetItemsByCategoriesAsync(List<ItemCategoryDto> category);

        Task<IEnumerable<ItemDto>> GetItemsByAuctionIDAsync(int auctionID);

        Task<IEnumerable<ItemDto>> ListFilteredItems(ItemFilterDto filter);

    }
}
