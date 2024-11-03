using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Test_Site_1.Application.Interfaces.FacadPatterns;
using Test_Site_1.Application.Services.Products.Commands.AddNewProduct;

using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using static Test_Site_1.Application.Services.Products.Commands.AddNewProduct.AddNewProductService;

using Test_Site_1.Domain.Entities.Products;
using Azure.Core;
using Test_Site_1.Application.Services.Products.Commands.RemoveProduct;
using static Test_Site_1.Application.Services.Products.Commands.EditProduct.EditProductService;



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

        //[HttpPost]

        //public IActionResult DeleteProduct(long ProductId)
        //{

        //    Console.WriteLine( ProductId);
        //    var result = _productFacad.RemoveProductService.Execute(ProductId);
        //    Console.WriteLine(result);


        //    if (ProductId == null)
        //    {
        //        return Json(new { isSuccess = false, message = "شناسه کالا معتبر نیست" });
        //    }

        //    Console.WriteLine("ProductId received in DeleteProduct action: " + ProductId); // Debugging log



        //    if (result == null)
        //    {
        //        return Json(new { isSuccess = false, message = "در کنترلر کالا یافت نشد " });
        //    }

        //    return Json(result);
        //}

        [HttpPost]
        public IActionResult DeleteProduct( long productId)
        {
            if (productId == null)
            {
                return Json(new { isSuccess = false, message = "شناسه کالا معتبر نیست" });
            }

            var result = _productFacad.RemoveProductService.Execute(productId);

            if (result == null  )
            {
                return Json(new { isSuccess = false, message = "در کنترلر کالا یافت نشد شیئ پوچ است "  });
            }

            if (!result.IsSuccess)
            {
                return Json(new { isSuccess = false, message = "در کنترلر کالا بافت نشد موفقییت امیز نبود " });
            }

            return Json(new { isSuccess = true, message = "کالا با موفقیت حذف شد" });
        }


        [HttpPost]
        public IActionResult EditProduct(RequestEditProductDto request) 
        {
            if (request.ProductId == null || string.IsNullOrEmpty(request.Name))
            {
                return Json(new { isSuccess = false, message = "شناسه محصول یا نام معتبر نیست" });
            }
            var result = _productFacad.EditProductService.Execute(
                new RequestEditProductDto
                {
                    ProductId = request.ProductId,
                    Name = request.Name,
                    Description = request.Description,
                    Inventory = request.Inventory,
                    Display = request.Display,
                    Brand = request.Brand,
                    Price = request.Price,
                    CategoryId = request.CategoryId,
                    ProductFeatures = request.ProductFeatures,
                    ProductImages = request.ProductImages,
                }
                );
            return Json(result);
               
                
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            var categoriesData = _productFacad.GetAllCategoriesService.Execute().Data;
            if (categoriesData != null)
            {
                ViewBag.Categories = new SelectList(categoriesData, "Id", "Name");
            }
            else 
            {
                ViewBag.Categories = new SelectList(Enumerable.Empty<SelectListItem>());
            }
            return View();
            //Bageto Method
            //ViewBag.Categories = new SelectList(_productFacad.GetAllCategoriesService.Execute().Data, "Id", "Name");
            //return View();
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
