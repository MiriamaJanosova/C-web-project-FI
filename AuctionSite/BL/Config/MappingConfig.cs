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
            config.CreateMap<Auction, AuctionBasicInfo>();

        }
    }
}