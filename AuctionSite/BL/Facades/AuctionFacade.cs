using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Base;
using BL.DTOs.Filter;
using BL.Facades.Base;
using BL.Services.Auctions;
using DAL.Entities;
using Infrastructure.UnitOfWork;

namespace BL.Facades
{
    public class AuctionFacade : FacadeBase<AuctionDto, AuctionFilterDto>
    {
        private IAuctionService _service;

        public AuctionFacade(IUnitOfWorkProvider unitOfWorkProvider, IAuctionService service) 
            : base(unitOfWorkProvider, service)
        {
            _service = service;
        }


        public async Task<AuctionDto> GetAuctionById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await _service.GetAsync(id);
            }
        }


    }
}
