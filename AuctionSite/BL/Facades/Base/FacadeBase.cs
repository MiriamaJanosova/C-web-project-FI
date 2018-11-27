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

        protected FacadeBase(IUnitOfWorkProvider unitOfWorkProvider)
        {
            UnitOfWorkProvider = unitOfWorkProvider;

        }
        
    }
}