using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using BL.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Common;
using BL.DTOs.Users;
using DAL.Entities;

namespace BL.Services.Users
{
    public interface IUserService : IService<UserDto, UserFilterDto>
    {
        /// <summary>
        /// Gets user with given email address
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>User with given email address</returns>
        Task<UserDto> GetUserAccordingToEmailAsync(string email);

        /// <summary>
        /// Gets DTO representing the entity according to Id
        /// </summary>
        /// <param name="entityId">entity Id</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<UserDto> GetAsync(int entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        User Create(UserDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(UserDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(int entityId);

        Task<IEnumerable<UserDto>> GetUserAccordingToNameAsync(string searchedName);
        Task<IEnumerable<AuctionDto>> GetAuctionsForUser(int id);
        Task<IEnumerable<UserDto>> ListFilteredUsers(UserFilterDto filter);
        UserShowSettingPage ConvertUserDtoForSettingPage(UserDto dto);

    }
}
