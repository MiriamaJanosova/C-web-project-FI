using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.Facades.Base;
using BL.QueryObjects.Common;
using BL.Services.Auctions;
using BL.Services.Categories;
using BL.Services.Items;
using BL.Services.Raises;
using BL.Services.Reviews;
using DAL.Entities;
using Infrastructure.UnitOfWork;
namespace BL.Facades
{
    public class RaiseFacade : FacadeBase
    {
        private readonly IRaiseService raiseService;

        public RaiseFacade(IUnitOfWorkProvider unitOfWorkProvider, IAuctionService auctionService,
            IRaiseService raiseService)
            : base(unitOfWorkProvider)
        {
            this.raiseService = raiseService;
        }

        public async Task<IEnumerable<RaiseDto>> GetRaisesForAuction(int auctionId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var raises = await raiseService.GetRaisesByAuctionIDAsync(auctionId);
                return raises.Items;
            }
        }
    }
}
