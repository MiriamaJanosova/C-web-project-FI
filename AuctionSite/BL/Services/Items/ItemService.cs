using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Items
{
    public class ItemService :
        CrudQueryServiceBase<Item, ItemDto, ItemFilterDto>,
        IItemService
    {
        public ItemService(IMapper mapper, IRepository<Item> itemRepository, 
            QueryObjectBase<ItemDto, Item, ItemFilterDto, IQuery<Item>> itemListQuery)
            : base(mapper, itemRepository, itemListQuery) { }

        public async Task<ItemDto> GetItemsByNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new ItemFilterDto { SearchedName = name });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<QueryResultDto<ItemDto, ItemFilterDto>> ListAllAsync(ItemFilterDto item)
        {
            return await Query.ExecuteQuery(item);
        }

        protected override Task<Item> GetWithIncludesAsync(int entityId)
        {
            return Repository.GetAsync(entityId);
        }
    }
}
