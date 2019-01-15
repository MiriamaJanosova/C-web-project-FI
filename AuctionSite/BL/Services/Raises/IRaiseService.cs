using System.Collections.Generic;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.QueryObjects.Common;
using BL.Services.Common;

namespace BL.Services.Raises
{
    public interface IRaiseService : IService<RaiseDto, RaiseFilterDto>
    {

        Task<QueryResultDto<RaiseDto, RaiseFilterDto>> GetRaisesByAuctionIDAsync(int AuctionID);

        /// <summary>
        /// Gets DTO representing the entity according to Id
        /// </summary>
        /// <param name="entityId">entity Id</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<RaiseDto> GetAsync(int entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        DAL.Entities.Raise Create(RaiseDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(RaiseDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(int entityId);

        Task<QueryResultDto<RaiseDto, RaiseFilterDto>> GetRaisesByUserIDAsync(int userID);

        Task<QueryResultDto<RaiseDto, RaiseFilterDto>> GetUserRaisesForAuction(int userID, int auctionID);

        Task<QueryResultDto<RaiseDto, RaiseFilterDto>> GetRaisesByAuctionIDFromOldest(int auctionID);

        Task<IEnumerable<RaiseDto>> GetRaisesByPriceAsync(double price);
    }
}