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
using BL.QueryObjects.Common;
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

    public class UserFacade : FacadeBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private Func<IdentityUserManager> UserManagerFactory { get; set; }

        public UserFacade(IUnitOfWorkProvider unitOfWorkProvider, IUserService userService, IMapper mapper, Func<IdentityRoleManager> roleManagerFactory, Func<IdentityUserManager> userManagerFactory)
            : base(unitOfWorkProvider)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.UserManagerFactory = userManagerFactory;
        }

        // TODO - urcite move mapper do servisy, nie tu
        public async Task<IdentityResult> CreateAsync(CreateUser dto)
        {
            using (UnitOfWorkProvider.Create())
            {
                using (var manager = UserManagerFactory.Invoke())
                {
                    var user = mapper.Map<User>(dto);
                    
                    return await manager.CreateAsync(user, dto.Password);
                }
            }
        }

        public async Task<UserDto> GetUserAccordingToEmailAsync(string email)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.GetUserAccordingToEmailAsync(email);
            }
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.GetAsync(id);
            }
        }


        public async Task<IEnumerable<UserDto>> GetUserAccordingToUserNameAsync(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.GetUserAccordingToNameAsync(name);
            }
        }

        public async Task<QueryResultDto<UserDto, UserFilterDto>> GetAllUsersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.ListAllAsync();
            }
        }

        public async Task<IEnumerable<AuctionDto>> GetAuctionsForUser(UserDto user)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await userService.GetAuctionsForUser(user.Id);
            }
        }

        public async Task<IEnumerable<UserDto>> GetFilteredUsersAsync(UserFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                if (filter == null)
                {
                    var temp = await userService.ListAllAsync();
                    return temp.Items;
                }

                return await userService.ListFilteredUsers(filter);
            }
        }

        public ClaimsIdentity Login(string email, string password)
        {
            using ( UnitOfWorkProvider.Create())
            using (var userManager = UserManagerFactory.Invoke())
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
                var l = await userService.ListAllAsync();
                return l.Items;
            }
        }

        public UserShowSettingPage ConvertUserDtoToSettingPage(UserDto dto)
        {
            return userService.ConvertFromTo(dto, new UserShowSettingPage());
        }

        public async Task UpdateUserInfo(UserShowSettingPage dto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var user = await userService.GetAsync(dto.Id);
                await userService.Update(userService.ConvertFromTo(dto, user));
                await uow.Commit();
            }
        }
    }
}
