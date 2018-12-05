using AutoMapper;
using BL.DTOs.Common;

namespace BL.Services
{
    public abstract class ServiceBase<TDto>
        where TDto : DtoBase
    {
        protected readonly IMapper Mapper;

        protected ServiceBase(IMapper mapper)
        {
            this.Mapper = mapper;
        }
        
        public TO ConvertFromTo<TFrom, TO>(TFrom source, TO destination)
        {
            return Mapper.Map(source, destination);
        }

        public TDto MapToBase<From>(From source)
        {
            return Mapper.Map<From, TDto>(source);
        }
    }
}