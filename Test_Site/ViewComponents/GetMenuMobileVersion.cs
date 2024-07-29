using Microsoft.AspNetCore.Mvc;
using Test_Site_1.Application.Services.Common.Queries.GetMenuItem;

namespace Test_Site.ViewComponents
{
    public class GetMenuMobileVersion : ViewComponent
    {
        private readonly IGetMenuItemService _getmenuItemService;
        public GetMenuMobileVersion(IGetMenuItemService getMenuItemService)
        {
            _getmenuItemService = getMenuItemService;   
        }
        public IViewComponentResult Invoke() 
        {
            var menuItem = _getmenuItemService.Execute();
            return View(viewName: "GetMenuMobileVersion",model: menuItem.Data);
        }


    }
}
