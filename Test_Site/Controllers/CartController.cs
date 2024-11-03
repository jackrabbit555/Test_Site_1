using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Test_Site_1.Application.Services.Carts;
using Test_Site.Utilities;

namespace Test_Site.Controllers
{
    public class CartController : Controller
    {


        private readonly ICartService _cartService;
        private readonly CookiesManeger _cookiesManeger;

        public CartController(ICartService cartService )
        {
            _cartService = cartService;
            _cookiesManeger = new CookiesManeger();
        }
        public IActionResult Index()
        {
            var userId = ClaimUtility.GetUserId(User);


            var result = _cartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext),userId);


            return View(result.Data);
        }


        public IActionResult AddToCart (long ProductId) 
        
        {
           
            var resultAdd = _cartService.AddToCart(ProductId, _cookiesManeger.GetBrowserId(HttpContext));
         

            return RedirectToAction("index");
        }

        public IActionResult Add(long CartItemId) 
       
        
        {
            _cartService.Add(CartItemId);
            return RedirectToAction("index");

        }
        public IActionResult Lowoff(long CartItemId) 
        {
            _cartService.LowOff(CartItemId);
            return RedirectToAction("index");
        }

        public IActionResult Remove(long ProductId ) 
        {
            _cartService.RemoveFromCart(ProductId, _cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction ("index");
        }
    }
}
