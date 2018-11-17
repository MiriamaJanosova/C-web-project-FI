using BL.DTOs.Common;
using BL.Services;
using BL.Services.Common;
using Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades.Base
{
    public abstract class FacadeBase<TDto, TFilterDto> 
        where TFilterDto : FilterDtoBase, new()
        where TDto : DtoBase
    {
        protected readonly IUnitOfWorkProvider UnitOfWorkProvider;
        protected readonly IService<TDto, TFilterDto> Service;

        protected FacadeBase(IUnitOfWorkProvider unitOfWorkProvider, 
            IService<TDto, TFilterDto> service)
        {
            UnitOfWorkProvider = unitOfWorkProvider;
            Service = service;

        }

        public async Task<IEnumerable<TDto>> GetAll()
        {
            using (UnitOfWorkProvider.Create())
            {
                var list = await Service.ListAllAsync();
                return list.Items;
            }
        }
    }
}