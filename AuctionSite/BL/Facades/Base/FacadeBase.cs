using BL.DTOs.Common;
using BL.Services;
using BL.Services.Common;
using Infrastructure.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Facades.Base
{
    public abstract class FacadeBase
    {
        protected readonly IUnitOfWorkProvider UnitOfWorkProvider;

        protected FacadeBase(IUnitOfWorkProvider unitOfWorkProvider)
        {
            UnitOfWorkProvider = unitOfWorkProvider;

        }
    }
}