using Microsoft.AspNetCore.Mvc;
using Test_Site_1.Application.Services.HomePage.AddHomePageImage;
using Test_Site_1.Domain.Entities.HomePage;

namespace Test_Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageImagesController : Controller
    {
        private readonly IAddHomePageImageService _addHomePageImageService;
        public HomePageImagesController(IAddHomePageImageService addHomePageImageService)
        {
            _addHomePageImageService = addHomePageImageService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add() 
        
        {

            return View();
        }

        [HttpPost]
        public IActionResult Add(IFormFile file,string link , ImageLocation imageLocation) 
        {
            _addHomePageImageService.Execute(new requestAddHomePageImagesDto
            {
                File = file,
                ImageLocation = imageLocation,
                Link = link

            });
            return View();
        }
    }
}
