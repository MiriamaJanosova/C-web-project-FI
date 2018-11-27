using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using IdentityResult = Microsoft.AspNet.Identity.IdentityResult;

namespace BL.Facades
{
    public class UserFacade : FacadeBase<UserDto, UserFilterDto>
    {
        private IUserService _service;
        private readonly IMapper _mapper;
        private Func<IdentityUserManager> UserManagerFactory{ get;}

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService service, IMapper mapper, 
            Func<IdentityUserManager> userManagerFactory) 
            : base(unitOfWorkProvider)
        {
            _service = service;
            _mapper = mapper;
            UserManagerFactory = userManagerFactory;
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

        public ClaimsIdentity Login(string email, string password)
        {
            using ( UnitOfWorkProvider.Create())
            using (var userManager = UserManagerFactory())
            {
                var user = userManager.Find(email, password);
                var result = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                return result;
            }

        }
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                var l = await _service.ListAllAsync();
                return l.Items;
            }
        }
    }
}
