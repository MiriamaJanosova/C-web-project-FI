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

        public async Task<IEnumerable<UserDto>> GetUserAccordingToNameAsync(string searchedName)
        {
            var queryResult = await Query.ExecuteQuery(new UserFilterDto { UserName = searchedName });
            return queryResult.Items;
        }

        public async Task<IEnumerable<AuctionDto>> GetAuctionsForUser(int id)
        {
            throw new NotImplementedException();
            var queryResult = await Query.ExecuteQuery(new UserFilterDto { ID = id });
            //return queryResult.Items.Select();
        }

        public async Task<IEnumerable<UserDto>> ListFilteredUsers(UserFilterDto filter)
        {
            var queryResult = await Query.ExecuteQuery(filter);
            return queryResult.Items;
        }


        protected override async Task<User> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId);
        }
    }
}
