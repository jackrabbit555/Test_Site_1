using Microsoft.AspNetCore.Mvc;
using Test_Site_1.Application.Services.Common.Queries.GetCategory;
using Test_Site_1.Application.Services.Products.Queries.GetAllCategories;

namespace Test_Site.ViewComponents
{
    public class Search : ViewComponent
    {
        private readonly IGetCategoryService _getCategoryService;
        public Search(IGetCategoryService getCategoryService) 
        {
            _getCategoryService = getCategoryService;
        }

        public IViewComponentResult Invoke()
        {
            return View(viewName: "Search", _getCategoryService.Execute().Data);
        }

    }
}
