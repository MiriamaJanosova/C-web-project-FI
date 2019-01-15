using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BL.Facades;
using PL.Controllers.Common;
using PL.Models.Raise;

namespace PL.Controllers
{
    [AllowAnonymous]
    public class RaiseController : BaseController
    {
        public RaiseFacade RaiseFacade { get; set; }
        public async Task<ActionResult> Index(int auctionId, int page)
        {
            var raises = await RaiseFacade.GetRaisesForAuction(auctionId);
            if (raises == null)
            {
                return View("RaiseList", new RaisesListViewModel());
            }

            return View("RaiseList",
                new RaisesListViewModel(raises, page, 10, raises.Count()));

        }
    }
}