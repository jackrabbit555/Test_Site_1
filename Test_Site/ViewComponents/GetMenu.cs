using Microsoft.AspNetCore.Mvc;
using Test_Site_1.Application.Services.Common.Queries.GetMenuItem;

namespace Test_Site.ViewComponents
{
    public class GetMenu : ViewComponent
    {
        private readonly IGetMenuItemService _getMenuItemService;

        public GetMenu(IGetMenuItemService getMenuItemService)
        {
            _getMenuItemService = getMenuItemService;
        }

        public IViewComponentResult Invoke()
        {
            var menuItem = _getMenuItemService.Execute();
            return View(viewName: "GetMenu", model: menuItem.Data);
        }
    }
}
