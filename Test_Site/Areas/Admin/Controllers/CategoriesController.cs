using Microsoft.AspNetCore.Mvc;
using Test_Site_1.Application.Interfaces.FacadPatterns;
using Test_Site_1.Application.Services.Products.Commands.EditCategory;

namespace Test_Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        
        private readonly IProductFacad _productFacad;
        public CategoriesController(IProductFacad productFacad)
        {
                _productFacad = productFacad;
        }

        public IActionResult Index(long? parentId)
        {
            return View(_productFacad.GetCategoriesService.Execute(parentId).Data);
        }


        

        [HttpGet]
        public IActionResult AddNewCategory(long? ParentId ) 
        {
            ViewBag.parentId = ParentId;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewCategory(long? ParentId , string Name) 
        { 
        var result = _productFacad.AddNewCategoryService.Execute( ParentId, Name );
            return Json(result);
        }
        [HttpPost]

        public IActionResult DeleteCategory(long? CategoryId) 
        {
            if (CategoryId == null)
            {
                return Json(new { isSuccess = false, message = "شناسه دسته بندی معتبر نیست" });
            }
            Console.WriteLine("CategoryId received in DeleteCategory action: " + CategoryId); // بررسی مقدار دریافت شده
           
            var result = _productFacad.RemoveCategoryService.Execute(CategoryId);
            return Json(result);
        }
        [HttpPost]
        public IActionResult EditCategory(long? categoryId, string name)
        {
            if (categoryId == null || string.IsNullOrEmpty(name))
            {
                return Json(new { isSuccess = false, message = "شناسه دسته بندی یا نام معتبر نیست" });
            }

            var result = _productFacad.EditCategoryService.Execute(new RequestEditCategoryDto
            {
                CategoryID = categoryId,
                CategoryName = name
            });

            return Json(result);
        }



    }
}
