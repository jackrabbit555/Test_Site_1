using Microsoft.EntityFrameworkCore;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;

namespace Test_Site_1.Application.Services.Common.Queries.GetMenuItem
{
    public interface IGetMenuItemService

    {
        ResultDto<List<MenuItemDto>> Execute();

    }

    public class GetMenuItemService : IGetMenuItemService
    {
        private readonly IDataBaseContext _context;

        public GetMenuItemService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<MenuItemDto>> Execute()
        {
            var category = _context.Categories
                .Include(p => p.SubCategories)
                .Where(p => p.ParentCategoryId == null)
                .ToList()
                .Select(p => new MenuItemDto
                {
                    CatId = p.Id,
                    Name = p.Name,
                    Child = p.SubCategories.ToList().Select(child => new MenuItemDto
                    {
                        CatId = child.Id,
                        Name = child.Name,
                    }).ToList(),
                }).ToList();
            return new ResultDto<List<MenuItemDto>>()
            {
                Data = category,
                IsSuccess = true

            };
        }


    }
}



public class MenuItemDto
{
    public long CatId { get; set; }
    public string Name { get; set; }
    public List<MenuItemDto> Child { get; set; }
}


