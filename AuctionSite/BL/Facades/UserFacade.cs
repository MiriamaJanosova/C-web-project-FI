using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.Facades.Base;
using BL.Services.Users;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class UserFacade : FacadeBase
    {
        private IUserService _service;

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService service) : base(unitOfWorkProvider)
        {
            _service = service;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            using (UnitOfWorkProvider.Create())
            {
                var l = await _service.ListAllAsync();
                return l.Items;
            }
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
