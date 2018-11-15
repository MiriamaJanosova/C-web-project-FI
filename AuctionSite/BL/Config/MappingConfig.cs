using System.IO.Compression;
using AutoMapper;
using BL.DTOs;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure.Query;

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
            config.CreateMap<QueryResult<Item>, QueryResultDto<ItemDto, ItemFilterDto>>();
            config.CreateMap<QueryResult<Category>, QueryResultDto<CategoryDto, CategoryFilterDto>>();
            config.CreateMap<QueryResult<Review>, QueryResultDto<ReviewDto, ReviewFilterDto>>();
            config.CreateMap<QueryResult<User>, QueryResultDto<UserDto, UserFilterDto>>();
            config.CreateMap<QueryResult<Auction>, QueryResultDto<AuctionDto, AuctionFilterDto>>();

        }

        public static void Initialize()
        {
            Mapper.Initialize(config =>
                config.CreateMap<Auction, AuctionDto>().ReverseMap()
            );
        }
    }
}