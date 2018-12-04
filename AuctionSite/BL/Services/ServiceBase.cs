using AutoMapper;

namespace BL.Services
{
    public abstract class ServiceBase
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
    }
}