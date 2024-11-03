using Microsoft.AspNetCore.Mvc;
using Test_Site.Utilities;
using Test_Site_1.Application.Services.Carts;
using Test_Site_1.Domain.Entities.Users;

namespace Test_Site.ViewComponents
{

    public class Cart : ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly CookiesManeger _cookiesManeger;


        public Cart(ICartService cartService,CookiesManeger cookiesManeger)
        {
            _cartService = cartService;
            _cookiesManeger = cookiesManeger; 


        }

        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            var browserId = _cookiesManeger.GetBrowserId(HttpContext);
            var cartData = _cartService.GetMyCart(browserId,userId).Data;
            return View(viewName: "Cart", cartData);

        }
    }
}
