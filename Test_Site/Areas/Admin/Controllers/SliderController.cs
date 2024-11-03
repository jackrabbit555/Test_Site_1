using Microsoft.AspNetCore.Mvc;
using Test_Site_1.Application.Services.HomePage.AddNewSlider;

namespace Test_Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
       
        private readonly IAddNewSliderService _addNewSliderService;
        public SliderController(IAddNewSliderService addNewSliderService)
        {
            _addNewSliderService = addNewSliderService;
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
        public IActionResult Add(IFormFile file, string Link) 
        {
            _addNewSliderService.Execute(file, Link);
            return View();
        }
    }
}
