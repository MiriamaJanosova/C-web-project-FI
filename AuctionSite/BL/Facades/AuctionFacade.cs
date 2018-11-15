using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.Facades.Base;
using BL.Services.Auctions;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class AuctionFacade : FacadeBase
    {
        private IAuctionService _service;

        public AuctionFacade(IUnitOfWorkProvider unitOfWorkProvider, IAuctionService service) : base(unitOfWorkProvider)
        {
            _service = service;
        }

        public async Task<IEnumerable<AuctionDto>> GetAllAuctions()
        {
            using (UnitOfWorkProvider.Create())
            {
                var list = await _service.ListAllAsync();
                return list.Items;
            }
        }


    }
}
