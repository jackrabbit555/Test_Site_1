using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Test_Site_1.Application.Interfaces.FacadPatterns;
using Test_Site_1.Application.Services.Products.Queries.GetProductForSite ;



namespace Test_Site.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProductFacad _productFacad;


        public ProductsController(IProductFacad productFacad )
        {
            _productFacad = productFacad;
        }
        public IActionResult Index(string searchKey, Ordering ordering , long? catId = null, int page = 1, int pageSize = 20)
        {

            var result = _productFacad.GetProductForSiteService.Execute( ordering , searchKey, page, catId, pageSize).Data;

            return View(result);
        }


        public IActionResult Detail(long Id) 
        {
            var result = _productFacad.GetProductDeatilForSiteService.Execute(Id).Data;
            return View(result);    
        } 

        //public IActionResult Deatil(long Id) 
        //{
        //    return View(_productFacad.getpro.Execute(Id).Data);
        //}

    }
}
