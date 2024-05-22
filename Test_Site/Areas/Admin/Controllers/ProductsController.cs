using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Test_Site_1.Application.Interfaces.FacadPatterns;
using Test_Site_1.Application.Services.Products.Commands.AddNewProduct;
using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using static Test_Site_1.Application.Services.Products.Commands.AddNewProduct.AddNewProductService;
using Test_Site_1.Domain.Entities.Products;



namespace Test_Site.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductsController : Controller
    {

        private readonly IProductFacad _productFacad;

        public ProductsController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }
        public IActionResult Index(int page = 1 , int pageSize = 20)
        {
            var result = _productFacad.GetProductForAdminService.Execute(page, pageSize).Data;   

            return View(result);
        }


        public IActionResult Detail(long Id)
        {
            var result = _productFacad.GetProductDetailForAdmin.Execute(Id).Data;
            return View(result);
        }

        [HttpPost]
        public IActionResult DeleteProduct(long? productId)
        {
            if (productId == null)
            {
                return Json(new { isSuccess = false, message = "شناسه کالا معتبر نیست" });
            }
            Console.WriteLine("CategoryId received in DeleteCategory action: " + productId); // بررسی مقدار دریافت شده
            var result = _productFacad.RemoveProductService.Execute(productId);
            return View(result);
                
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.Categories = new SelectList(_productFacad.GetAllCategoriesService.Execute().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(RequestAddNewProductDto request, List<AddNewProduct_Features> Features)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.Images = images;
            request.Features = Features;
            return Json(_productFacad.AddNewProductService.Execute(request));
        }
    }

}
