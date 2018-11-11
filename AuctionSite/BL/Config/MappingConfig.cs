using System.IO.Compression;
using AutoMapper;
using BL.DTOs;
using BL.DTOs.Base;
using DAL.Entities;

namespace BL.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Auction, AuctionDto>().ReverseMap();
            config.CreateMap<Category, CategoryDto>().ReverseMap();
            config.CreateMap<Currency, CurrencyDto>().ReverseMap();
            config.CreateMap<EmailTemplate, EmailTemplateDto>().ReverseMap();
            config.CreateMap<ItemCategory, ItemCategoryDto>().ReverseMap();
            config.CreateMap<Item, ItemDto>().ReverseMap();
            config.CreateMap<Raise, RaiseDto>().ReverseMap();
            config.CreateMap<Review, ReviewDto>().ReverseMap();
            config.CreateMap<Role, RoleDto>().ReverseMap();
            config.CreateMap<UserRole, UserRoleDto>().ReverseMap();
            config.CreateMap<User, UserDto>().ReverseMap();

        }
    }
}

/**config.CreateMap<QueryResult<Product>, QueryResultDto<ProductDto, ProductFilterDto>>();
  *          config.CreateMap<QueryResult<Category>, QueryResultDto<CategoryDto, CategoryFilterDto>>();
  *         config.CreateMap<QueryResult<Order>, QueryResultDto<OrderDto, OrderFilterDto>>();
  *          config.CreateMap<QueryResult<OrderItem>, QueryResultDto<OrderItemDto, OrderItemFilterDto>>();
  *          config.CreateMap<QueryResult<Customer>, QueryResultDto<CustomerDto, CustomerFilterDto>>();
  */