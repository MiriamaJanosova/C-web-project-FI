using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.DTOs.Users;
using BL.Facades.Base;
using BL.Identity;
using BL.Services.Users;
using DAL.Entities;
using Infrastructure.UnitOfWork;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BL.Facades
{
    public class UserFacade : FacadeBase<UserDto, UserFilterDto>
    {
        private IUserService _service;
        private readonly IMapper _mapper;
        private Func<IdentityUserManager> UserManagerFactory{ get; set; }

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService service, IMapper mapper) 
            : base(unitOfWorkProvider, service)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _service.GetAsync(id);
            }
        }

        // TODO - move to base possibly
        public async Task<IdentityResult> CreateAsync(CreateUser dto)
        {
            using (UnitOfWorkProvider.Create())
            {
                using (var manager = UserManagerFactory())
                {
                    var user = _mapper.Map<User>(dto);
                    user.UserName = user.Email;

                    return await manager.CreateAsync(user, dto.Password);
                }
            }
        }
    }
}
