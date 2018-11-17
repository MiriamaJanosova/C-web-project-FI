using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.Facades.Base;
using BL.Services.Users;
using DAL.Entities;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class UserFacade : FacadeBase<UserDto, UserFilterDto>
    {
        private IUserService _service;

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService service) 
            : base(unitOfWorkProvider, service)
        {
            _service = service;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _service.GetAsync(id);
            }
        }
    }
}
