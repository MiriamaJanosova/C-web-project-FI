using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Common;
using BL.QueryObjects.Common;

namespace BL.Services.Common
{
    public interface IService<TDto, TFilterDto>
        where TFilterDto : FilterDtoBase, new()
        where TDto : DtoBase
    {
        Task<QueryResultDto<TDto, TFilterDto>> ListAllAsync();
        TO ConvertFromTo<TFrom, TO>(TFrom source, TO destination);

        TDto MapToBase<From>(From source);
    }
}
