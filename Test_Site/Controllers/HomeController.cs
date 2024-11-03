using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System.Diagnostics;
using Test_Site.Models;
using Test_Site.Models.ViewModels.HomePageViewModel;
using Test_Site_1.Application.Interfaces.FacadPatterns;
using Test_Site_1.Application.Services.Common.Queries.GetHomePageImage;
using Test_Site_1.Application.Services.Common.Queries.GetSlider;
using Test_Site_1.Application.Services.Products.Queries.GetProductForSite;

namespace Test_Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetSliderService _sliderService;  
        private readonly IGetHomePageImagesService _HomePageImagesService;
        private readonly IProductFacad _productFacad;

        public HomeController(IProductFacad productFacad,ILogger<HomeController> logger, IGetSliderService getSliderService ,IGetHomePageImagesService HomePageImagesService )
        {
            _logger = logger;
            _sliderService = getSliderService;
            _HomePageImagesService = HomePageImagesService;
            _productFacad = productFacad;

        }



        public IActionResult Read() 
        {
            var cookievalue = Request.Cookies["message"].ToString();
            return Ok(cookievalue);
        }
        public IActionResult Index()
        {
            Response.Cookies.Append("message", "wellcome", new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                Path = Request.PathBase.HasValue ? Request.PathBase.ToString() :  "/" , 
                Expires = DateTime.Now.AddDays(5)   
            });
            
            HomePageViewModel homePage = new HomePageViewModel()
            {
                sliderDtos = _sliderService.Execute().Data,
                PageImages = _HomePageImagesService.Execute().Data,
                Camera = _productFacad.GetProductForSiteService.Execute(Ordering.theNewest, null, 1, 6, null).Data.Products,
            };  
            return View(homePage);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            var errorViewModel = new ErrorViewModel { RequestId = requestId };
            _logger.LogInformation("Error view model created with RequestId: {RequestId}", requestId);
            return View(errorViewModel);
        }
    }
}
