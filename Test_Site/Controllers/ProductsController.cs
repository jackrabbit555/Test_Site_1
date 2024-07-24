using Microsoft.AspNetCore.Mvc;
using Test_Site_1.Application.Interfaces.FacadPatterns;

namespace Test_Site.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProductFacad _productFacad;


        public ProductsController(IProductFacad productFacad )
        {
            _productFacad = productFacad;
        }
        public IActionResult Index(string SearchKey, long? CatId = null  ,  int page = 1)
        {

            var result = _productFacad.GetProductForSiteService.Execute( SearchKey ,page , CatId).Data;

            return View(result);
        }


        //public IActionResult Deatil(long Id) 
        //{
        //    return View(_productFacad.getpro.Execute(Id).Data);
        //}

    }
}
