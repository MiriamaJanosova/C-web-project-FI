using AutoMapper;

namespace BL.Services
{
    public abstract class ServiceBase
    {
        protected readonly IMapper Fapper;

        protected ServiceBase(IMapper fapper)
        {
            this.Fapper = fapper;
        }
    }
}