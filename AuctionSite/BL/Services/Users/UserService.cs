using AutoMapper;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using DAL.Entities;
using Infrastructure;
using Infrastructure.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Users
{
    public class UserService : 
        CrudQueryServiceBase<User, UserDto, UserFilterDto>, 
        IUserService
    {
        public UserService(IMapper mapper, IRepository<User> customerRepository, 
            QueryObjectBase<UserDto, User, UserFilterDto, IQuery<User>> userQueryObject)
            : base(mapper, customerRepository, userQueryObject) { }

        public async Task<UserDto> GetUserAccordingToEmailAsync(string email)
        {
            var queryResult = await Query.ExecuteQuery(new UserFilterDto { UserEmail = email });
            return queryResult.Items.SingleOrDefault();
        }

        protected override async Task<User> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }
    }
}
