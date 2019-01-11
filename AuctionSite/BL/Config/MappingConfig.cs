using System;
using System.IO.Compression;
using AutoMapper;
using BL.DTOs;
using BL.DTOs.Auction;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.DTOs.Item;
using BL.DTOs.Users;
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
            config.CreateMap<Image, ImageDto>().ReverseMap();
            config.CreateMap<Category, CategoryDto>().ReverseMap();
            config.CreateMap<Currency, CurrencyDto>().ReverseMap();
            config.CreateMap<EmailTemplate, EmailTemplateDto>().ReverseMap();
            config.CreateMap<ItemCategory, ItemCategoryDto>().ReverseMap();
            config.CreateMap<ItemDto, Item>().ReverseMap();
            config.CreateMap<RaiseDto, Raise>().ReverseMap();
            config.CreateMap<Review, ReviewDto>().ReverseMap();
            config.CreateMap<Role, RoleDto>().ReverseMap();
            config.CreateMap<UserRole, UserRoleDto>().ReverseMap();
            config.CreateMap<User, UserDto>().ReverseMap();
            //config.CreateMap<UserDto, User>(MemberList.Destination);
            config.CreateMap<UserDto, UserShowSettingPage>().ReverseMap();
            config.CreateMap<QueryResult<Item>, QueryResultDto<ItemDto, ItemFilterDto>>();
            config.CreateMap<QueryResult<Category>, QueryResultDto<CategoryDto, CategoryFilterDto>>();
            config.CreateMap<QueryResult<Review>, QueryResultDto<ReviewDto, ReviewFilterDto>>();
            config.CreateMap<QueryResult<User>, QueryResultDto<UserDto, UserFilterDto>>();
            config.CreateMap<QueryResult<Auction>, QueryResultDto<AuctionDto, AuctionFilterDto>>();

            config.CreateMap<CreateUser, UserDto>().ReverseMap();
            config.CreateMap<CreateUser, User>();
            config.CreateMap<CreateItem, ItemDto>().ReverseMap();
            config.CreateMap<CreateItem, Item>();
            config.CreateMap<CreateAuction, AuctionDto>().ReverseMap();
            config.CreateMap<CreateAuction, Auction>();
        }

        public static void Initialize()
        {
            Mapper.Initialize(config => ConfigureMapping(config));
        }
    }
}