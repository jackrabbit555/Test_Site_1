using Microsoft.AspNetCore.Mvc;
using Test_Site.Utilities;
using Test_Site_1.Application.Services.Orders.Queries.GetUserOrders;

namespace Test_Site.Controllers
{
    public class OrderController : Controller
    {

        private readonly IGetUserOrderService _getUserOrderService;


        public OrderController(IGetUserOrderService getUserOrderService) 
        {
            _getUserOrderService = getUserOrderService;
        }

        public IActionResult Index()
        {
          long userId = ClaimUtility.GetUserId(User).Value;
            return View(_getUserOrderService.Execute(userId).Data);
        }
    }
}
