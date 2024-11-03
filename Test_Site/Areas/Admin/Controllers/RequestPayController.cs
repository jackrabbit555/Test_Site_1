using Microsoft.AspNetCore.Mvc;
using Test_Site_1.Application.Services.Finances.Queries.GetRequestPayService;

namespace Test_Site.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class RequestPayController : Controller
    {


        private readonly IGetRrquestPayForAdminService _getRrquestPayForAdminService;

        public RequestPayController(IGetRrquestPayForAdminService getRrquestPayForAdminService)
        {
            _getRrquestPayForAdminService = getRrquestPayForAdminService;
        }
        public IActionResult Index()
        {

            
            return View(_getRrquestPayForAdminService.Execute().Data);
        }
    }
}
